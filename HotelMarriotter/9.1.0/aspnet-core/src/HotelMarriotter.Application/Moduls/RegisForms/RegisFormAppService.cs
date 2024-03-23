using Abp.Domain.Repositories;
using HotelMarriotter.DbEntities;
using HotelMarriotter.Moduls.RegisForms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.RegisForms
{
    public class RegisFormAppService : HotelMarriotterAppServiceBase
    {
        private readonly IRepository<RegisForm> _regisFormRepository;

        public RegisFormAppService(IRepository<RegisForm> regisFormRepository)
        {
            _regisFormRepository = regisFormRepository;
        }

        public async Task<RegisFormDto> GetRegisFormById(int id)
        {
            try
            {
                var regisForm = await _regisFormRepository.GetAsync(id);

                if (regisForm == null)
                {
                    throw new Exception($"Không tìm thấy biểu mẫu đăng ký với Id {id}");
                }

                var regisFormDto = new RegisFormDto
                {
                    Id = regisForm.Id,
                    DateRegis = regisForm.DateRegis,
                    DepositAmount = regisForm.DepositAmount,
                    NumberOfPeople = regisForm.NumberOfPeople,
                    Note = regisForm.Note,
                    GuestId = regisForm.GuestId,
                    StaffId = regisForm.StaffId
                };

                return regisFormDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<RegisFormDto>> GetAllRegisForms()
        {
            try
            {
                var regisForms = await _regisFormRepository.GetAllListAsync();

                var dtoList = regisForms.Select(regisForm => new RegisFormDto
                {
                    Id = regisForm.Id,
                    DateRegis = regisForm.DateRegis,
                    DepositAmount = regisForm.DepositAmount,
                    NumberOfPeople = regisForm.NumberOfPeople,
                    Note = regisForm.Note,
                    GuestId = regisForm.GuestId,
                    StaffId = regisForm.StaffId
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task AddRegisForm(RegisFormDto input)
        {
            var regisForm = new RegisForm
            {
                DateRegis = input.DateRegis,
                DepositAmount = input.DepositAmount,
                NumberOfPeople = input.NumberOfPeople,
                Note = input.Note,
                GuestId = input.GuestId,
                StaffId = input.StaffId
            };
            await _regisFormRepository.InsertAsync(regisForm);
        }

        public async Task<bool> DeleteRegisForm(int id)
        {
            try
            {
                var regisForm = await _regisFormRepository.FirstOrDefaultAsync(id);
                if (regisForm == null)
                {
                    return false;
                }

                await _regisFormRepository.DeleteAsync(regisForm);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateRegisForm(RegisFormDto input)
        {
            try
            {
                var existingRegisForm = await _regisFormRepository.GetAsync(input.Id);

                if (existingRegisForm == null)
                {
                    return false;
                }

                existingRegisForm.DateRegis = input.DateRegis;
                existingRegisForm.DepositAmount = input.DepositAmount;
                existingRegisForm.NumberOfPeople = input.NumberOfPeople;
                existingRegisForm.Note = input.Note;
                existingRegisForm.GuestId = input.GuestId;
                existingRegisForm.StaffId = input.StaffId;

                await _regisFormRepository.UpdateAsync(existingRegisForm);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<RegisFormDto>> SearchRegisForms(string keyword)
        {
            try
            {
                var regisForms = await _regisFormRepository.GetAllListAsync();
                var filteredRegisForms = regisForms
                    .Where(regisForm =>
                        regisForm.DateRegis.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        regisForm.DepositAmount.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        regisForm.NumberOfPeople.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)
                        )
                    .ToList();

                var dtoList = filteredRegisForms.Select(regisForm => new RegisFormDto
                {
                    Id = regisForm.Id,
                    DateRegis = regisForm.DateRegis,
                    DepositAmount = regisForm.DepositAmount,
                    NumberOfPeople = regisForm.NumberOfPeople,
                    Note = regisForm.Note,
                    GuestId = regisForm.GuestId,
                    StaffId = regisForm.StaffId
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
