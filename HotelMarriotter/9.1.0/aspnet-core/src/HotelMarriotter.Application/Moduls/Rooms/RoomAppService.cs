using Abp.Domain.Repositories;
using HotelMarriotter.DbEntities;
using HotelMarriotter.Moduls.Rooms.Dto;
using HotelMarriotter.Moduls.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.Rooms
{
    public class RoomAppService : HotelMarriotterAppServiceBase
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<TypeRoom> _typeRoomRepository;

        public RoomAppService(IRepository<Room> roomRepository,IRepository<TypeRoom> typeRoomRepository)
        {
            _roomRepository = roomRepository;
            _typeRoomRepository = typeRoomRepository;
        }

        public async Task<RoomDto> GetRoomById(int id)
        {
            try
            {
                var room = await _roomRepository.GetAsync(id);

                if (room == null)
                {
                    throw new Exception($"Không tìm thấy phòng với Id {id}");
                }

                var roomDto = new RoomDto
                {
                    Id = room.Id,
                    NameRoom = room.NameRoom,
                    Situation = room.Situation,
                    ImageUrl = room.ImageUrl,
                    TypeRoomId = room.TypeRoomId, 
                    IsActive = room.IsActive
                };

                return roomDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        /*public async Task<List<RoomDto>> GetAllRooms()
        {
            try
            {
                var rooms = await _roomRepository.GetAllListAsync();

                var dtoList = rooms.Select(room => new RoomDto
                {
                    Id = room.Id,
                    NameRoom = room.NameRoom,
                    Situation = room.Situation,
                    ImageUrl = room.ImageUrl,
                    TypeRoomId = room.TypeRoomId, // Trường thêm vào để xác định loại phòng
                    IsActive = room.IsActive
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }*/
        public async Task<List<RoomDto>> GetAllRooms()
        {
            try
            {
                var rooms = await _roomRepository.GetAllIncluding(room => room.TypeRooms).ToListAsync(); // Lấy thông tin loại phòng

                var dtoList = rooms.Select(room => new RoomDto
                {
                    Id = room.Id,
                    NameRoom = room.NameRoom,
                    Situation = room.Situation,
                    ImageUrl = room.ImageUrl,
                    TypeRoomId = room.TypeRoomId,
                    IsActive = room.IsActive,
                    NameTypeRoom= room.TypeRooms?.NameTypeRoom // Thêm thông tin về tên loại phòng
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        /*public async Task AddRoom(RoomDto input)
        {
            // Kiểm tra xem người dùng đã chọn hình ảnh hay chưa
            if (input.ImageFile != null && input.ImageFile.Length > 0)
            {
                // Tạo một tên file duy nhất để lưu trữ hình ảnh
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(input.ImageFile.FileName);

                // Xác định đường dẫn để lưu trữ hình ảnh trên máy chủ
                var filePath = Path.Combine("uploads", fileName);

                // Lưu trữ hình ảnh trên máy chủ
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.ImageFile.CopyToAsync(stream);
                }

                // Lưu thông tin phòng vào cơ sở dữ liệu, bao gồm đường dẫn của hình ảnh
                var room = new Room
                {
                    NameRoom = input.NameRoom,
                    Situation = input.Situation,
                    ImageUrl = filePath, // Lưu đường dẫn của hình ảnh
                    TypeRoomId = input.TypeRoomId,
                    IsActive = input.IsActive
                };

                await _roomRepository.InsertAsync(room);
            }
            else
            {
                // Nếu người dùng không chọn hình ảnh, chỉ lưu thông tin phòng mà không có hình ảnh
                var room = new Room
                {
                    NameRoom = input.NameRoom,
                    Situation = input.Situation,
                    TypeRoomId = input.TypeRoomId,
                    IsActive = input.IsActive
                };

                await _roomRepository.InsertAsync(room);
            }
        }*/
        public async Task AddRoom(RoomDto input)
        {
            try
            {
                // Lấy TypeRoomId từ input hoặc từ bất kỳ nguồn dữ liệu nào khác (ví dụ: giao diện người dùng)
                int typeRoomId = input.TypeRoomId;

                // Lưu thông tin phòng vào cơ sở dữ liệu, bao gồm đường dẫn của hình ảnh và TypeRoomId
                var room = new Room
                {
                    NameRoom = input.NameRoom,
                    Situation = input.Situation,
                    ImageUrl = input.ImageUrl,
                    IsActive = input.IsActive,
                    TypeRoomId = typeRoomId  // Gán TypeRoomId cho phòng mới
                };

                await _roomRepository.InsertAsync(room);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: ghi log, hiển thị thông báo cho người dùng, vv.
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Ném lại ngoại lệ để bên gọi xử lý tiếp
            }
        }


        public async Task<bool> DeleteRoom(int id)
        {
            try
            {
                var room = await _roomRepository.FirstOrDefaultAsync(id);
                if (room == null)
                {
                    return false;
                }

                await _roomRepository.DeleteAsync(room);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateRoom(RoomDto input)
        {
            try
            {
                var existingRoom = await _roomRepository.GetAsync(input.Id);

                if (existingRoom == null)
                {
                    return false;
                }

                existingRoom.NameRoom = input.NameRoom;
                existingRoom.Situation = input.Situation;
                existingRoom.ImageUrl = input.ImageUrl;
                existingRoom.TypeRoomId = input.TypeRoomId; // Trường thêm vào để xác định loại phòng
                existingRoom.IsActive = input.IsActive;

                await _roomRepository.UpdateAsync(existingRoom);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<RoomDto>> SearchRooms(string keyword)
        {
            try
            {
                var rooms = await _roomRepository.GetAllListAsync();
                var filteredRooms = rooms
                    .Where(room =>
                        room.NameRoom.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        room.Situation.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var dtoList = filteredRooms.Select(room => new RoomDto
                {
                    Id = room.Id,
                    NameRoom = room.NameRoom,
                    Situation = room.Situation,
                    ImageUrl = room.ImageUrl,
                    TypeRoomId = room.TypeRoomId, // Trường thêm vào để xác định loại phòng
                    IsActive = room.IsActive
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
