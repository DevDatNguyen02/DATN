using Abp.Domain.Repositories;
using HotelMarriotter.DbEntities;
using HotelMarriotter.Moduls.TypeRooms.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.TypeRooms
{
    public class TypeRoomAppService :HotelMarriotterAppServiceBase
    {
        private readonly IRepository<TypeRoom> _typeroomRepository;

        public TypeRoomAppService(IRepository<TypeRoom> typeroomRepository)
        {
            _typeroomRepository = typeroomRepository;
        }

        public async Task<TypeRoomDto> GetTypeRoomById(int id)
        {
            try
            {
                var typeroom = await _typeroomRepository.GetAsync(id);

                if (typeroom == null)
                {
                    throw new Exception($"Không tìm thấy loại phòng với Id {id}");
                }

                var typeroomDto = new TypeRoomDto
                {
                    Id = typeroom.Id,
                    NameTypeRoom = typeroom.NameTypeRoom,
                    Price = typeroom.Price,
                    Area = typeroom.Area,
                    Description = typeroom.Description
                };

                return typeroomDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        /*public async Task<List<TypeRoomDto>> GetAllTypeRooms()
        {
            try
            {
                var typerooms = await _typeroomRepository.GetAllListAsync();

                var dtoList = typerooms.Select(typeroom => new TypeRoomDto
                {
                    Id = typeroom.Id,
                    NameTypeRoom = typeroom.NameTypeRoom,
                    Price = typeroom.Price,
                    Area = typeroom.Area,
                    Description = typeroom.Description
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }*/

        public async Task<List<TypeRoomDto>> GetAllTypeRooms(int pageNumber, int pageSize)
        {
            try
            {
                var totalCount = await _typeroomRepository.CountAsync();
                var typerooms = await _typeroomRepository.GetAll()
                    .Skip((pageNumber - 1) * pageSize) // Bỏ qua số lượng phần tử trên trang trước
                    .Take(pageSize) // Lấy số lượng phần tử trên trang hiện tại
                    .ToListAsync();

                var dtoList = typerooms.Select(typeroom => new TypeRoomDto
                {
                    Id = typeroom.Id,
                    NameTypeRoom = typeroom.NameTypeRoom,
                    Price = typeroom.Price,
                    Area = typeroom.Area,
                    Description = typeroom.Description
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task AddTypeRoom(TypeRoomDto input)
        {
            var typeroom = new TypeRoom
            {
                NameTypeRoom = input.NameTypeRoom,
                Price = input.Price,
                Area = input.Area,
                Description = input.Description
            };
            await _typeroomRepository.InsertAsync(typeroom);
        }

        public async Task<bool> DeleteTypeRoom(int id)
        {
            try
            {
                var typeroom = await _typeroomRepository.FirstOrDefaultAsync(id);
                if (typeroom == null)
                {
                    return false;
                }

                await _typeroomRepository.DeleteAsync(typeroom);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateTypeRoom(TypeRoomDto input)
        {
            try
            {
                var existingTypeRoom = await _typeroomRepository.GetAsync(input.Id);

                if (existingTypeRoom == null)
                {
                    return false;
                }

                existingTypeRoom.NameTypeRoom = input.NameTypeRoom;
                existingTypeRoom.Price = input.Price;
                existingTypeRoom.Area = input.Area;
                existingTypeRoom.Description = input.Description;

                await _typeroomRepository.UpdateAsync(existingTypeRoom);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<List<TypeRoomDto>> SearchTypeRooms(string keyword)
        {
            try
            {
                var typerooms = await _typeroomRepository.GetAllListAsync();
                var filteredTypeRooms = typerooms
                    .Where(typeroom =>
                        typeroom.NameTypeRoom.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        typeroom.Price.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        typeroom.Area.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var dtoList = filteredTypeRooms.Select(typeroom => new TypeRoomDto
                {
                    Id = typeroom.Id,
                    NameTypeRoom = typeroom.NameTypeRoom,
                    Price = typeroom.Price,
                    Area = typeroom.Area,
                    Description = typeroom.Description
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
