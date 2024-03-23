using Abp.Authorization;
using Abp.Domain.Repositories;
using HotelMarriotter.Authorization;
using HotelMarriotter.DbEntities;
using HotelMarriotter.Moduls.Staffs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.Staffs
{
    [AbpAuthorize(PermissionNames.Pages_Staffs)]
    public class StaffAppService : HotelMarriotterAppServiceBase
    {
        private readonly IRepository<Staff> _staffRepository;
        public StaffAppService(IRepository<Staff> staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<StaffDto> GetStaffById(int id)
        {
            try
            {
                var staff = await _staffRepository.GetAsync(id);

                if (staff == null)
                {
                    throw new Exception($"Không tìm thấy nhân viên với Id {id}");
                }

                var staffDto = new StaffDto
                {
                    Id = staff.Id,
                    Name = staff.Name,
                    Gender = staff.Gender,
                    Address = staff.Address,
                    Email = staff.Email,
                    PhoneNumber = staff.PhoneNumber,
                    CCCD = staff.CCCD
                };

                return staffDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<StaffDto>> GetAllStaffs()
        {
            try
            {
                var staffs = await _staffRepository.GetAllListAsync();

                var dtoList = staffs.Select(staff => new StaffDto
                {
                    Id = staff.Id,
                    Name = staff.Name,
                    Gender = staff.Gender,
                    Address = staff.Address,
                    Email = staff.Email,
                    PhoneNumber = staff.PhoneNumber,
                    CCCD = staff.CCCD

                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task AddStaff(StaffDto input)
        {
            var staff = new Staff
            {
                Name = input.Name,
                Gender = input.Gender,
                Address = input.Address,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                CCCD = input.CCCD
            };
            await _staffRepository.InsertAsync(staff);
        }

        public async Task<bool> DeleteStaff(int id)
        {
            try
            {
                var staff = await _staffRepository.FirstOrDefaultAsync(id);
                if (staff == null)
                {
                    return false;
                }

                await _staffRepository.DeleteAsync(staff);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateStaff(StaffDto input)
        {
            try
            {
                var existingStaff = await _staffRepository.GetAsync(input.Id);

                if (existingStaff == null)
                {
                    return false;
                }
                existingStaff.Name = input.Name;
                existingStaff.Gender = input.Gender;
                existingStaff.Address = input.Address;
                existingStaff.Email = input.Email;
                existingStaff.PhoneNumber = input.PhoneNumber;
                existingStaff.CCCD = input.CCCD;

                await _staffRepository.UpdateAsync(existingStaff);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<List<StaffDto>> SearchStaffs(string keyword)
        {
            try
            {
                var staffs = await _staffRepository.GetAllListAsync();
                var filteredStaffs = staffs
                    .Where(staff =>
                        staff.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        staff.PhoneNumber.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        staff.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var dtoList = filteredStaffs.Select(staff => new StaffDto
                {
                    Id = staff.Id,
                    Name = staff.Name,
                    Gender = staff.Gender,
                    Address = staff.Address,
                    Email = staff.Email,
                    PhoneNumber = staff.PhoneNumber,
                    CCCD = staff.CCCD
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
