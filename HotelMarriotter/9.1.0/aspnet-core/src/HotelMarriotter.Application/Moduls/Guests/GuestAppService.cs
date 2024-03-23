using Abp.Domain.Repositories;
using HotelMarriotter.DbEntities;
using HotelMarriotter.Moduls.Guests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.Guests
{
    public class GuestAppService : HotelMarriotterAppServiceBase
    {
        private readonly IRepository<Guest> _guestReposity;
        public GuestAppService(IRepository<Guest> guestRepository)
        {
            _guestReposity = guestRepository;
        }

        public async Task<GuestDto> GetGuestById(int id)
        {
            try
            {
                var guest = await _guestReposity.GetAsync(id);

                if (guest == null)
                {
                    throw new Exception($"Không tìm thấy khách hàng với Id {id}");
                }

                var guestDto = new GuestDto
                {
                    Id = guest.Id,
                    Name = guest.Name,
                    Gender = guest.Gender,
                    Address = guest.Address,
                    Email = guest.Email,
                    PhoneNumber = guest.PhoneNumber,
                    CCCD = guest.CCCD
                };

                return guestDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<GuestDto>> GetAllGuests()
        {
            try
            {
                var guests = await _guestReposity.GetAllListAsync();

                var dtoList = guests.Select(guest => new GuestDto
                {
                    Id = guest.Id,
                    Name = guest.Name,
                    Gender = guest.Gender,
                    Address = guest.Address,
                    Email = guest.Email,
                    PhoneNumber = guest.PhoneNumber,
                    CCCD = guest.CCCD
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task AddGuest(GuestDto input)
        {
            var guest = new Guest
            {
                Name = input.Name,
                Gender = input.Gender,
                Address = input.Address,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                CCCD = input.CCCD
            };
            await _guestReposity.InsertAsync(guest);
        }

        public async Task<bool> DeleteGuest(int id)
        {
            try
            {
                var guest = await _guestReposity.FirstOrDefaultAsync(id);
                if (guest == null)
                {
                    return false;
                }

                await _guestReposity.DeleteAsync(guest);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateGuest(GuestDto input)
        {
            try
            {
                var existingGuest = await _guestReposity.GetAsync(input.Id);

                if (existingGuest == null)
                {
                    return false;
                }
                existingGuest.Name = input.Name;
                existingGuest.Gender = input.Gender;
                existingGuest.Address = input.Address;
                existingGuest.Email = input.Email;
                existingGuest.PhoneNumber = input.PhoneNumber;
                existingGuest.CCCD = input.CCCD;

                await _guestReposity.UpdateAsync(existingGuest);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<List<GuestDto>> SearchGuets(string keyword)
        {
            try
            {
                var staffs = await _guestReposity.GetAllListAsync();
                var filteredStaffs = staffs
                    .Where(staff =>
                        staff.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        staff.PhoneNumber.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        staff.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var dtoList = filteredStaffs.Select(guest => new GuestDto
                {
                    Id = guest.Id,
                    Name = guest.Name,
                    Gender = guest.Gender,
                    Address = guest.Address,
                    Email = guest.Email,
                    PhoneNumber = guest.PhoneNumber,
                    CCCD = guest.CCCD
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
