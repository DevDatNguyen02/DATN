using Abp.Domain.Repositories;
using HotelMarriotter.DbEntities;
using HotelMarriotter.Moduls.Invoices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.Invoices
{
    public class InvoiceAppService : HotelMarriotterAppServiceBase
    {
        private readonly IRepository<Invoice> _invoiceRepository;

        public InvoiceAppService(IRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDto> GetInvoiceById(int id)
        {
            try
            {
                var invoice = await _invoiceRepository.GetAsync(id);

                if (invoice == null)
                {
                    throw new Exception($"Không tìm thấy hoá đơn với Id {id}");
                }

                var invoiceDto = new InvoiceDto
                {
                    Id = invoice.Id,
                    CreateTime = invoice.CreateTime,
                    TotalBill = invoice.TotalBill,
                    Note = invoice.Note,
                    RoomCharge = invoice.RoomCharge,
                    ServiceCharge = invoice.ServiceCharge,
                    DepositAmount = invoice.DepositAmount,
                    GuestId = invoice.GuestId,
                    StaffId = invoice.StaffId
                };

                return invoiceDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<InvoiceDto>> GetAllInvoices()
        {
            try
            {
                var invoices = await _invoiceRepository.GetAllListAsync();

                var dtoList = invoices.Select(invoice => new InvoiceDto
                {
                    Id = invoice.Id,
                    CreateTime = invoice.CreateTime,
                    TotalBill = invoice.TotalBill,
                    Note = invoice.Note,
                    RoomCharge = invoice.RoomCharge,
                    ServiceCharge = invoice.ServiceCharge,
                    DepositAmount = invoice.DepositAmount,
                    GuestId = invoice.GuestId,
                    StaffId = invoice.StaffId
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task AddInvoice(InvoiceDto input)
        {
            var invoice = new Invoice
            {
                CreateTime = input.CreateTime,
                TotalBill = input.TotalBill,
                Note = input.Note,
                RoomCharge = input.RoomCharge,
                ServiceCharge = input.ServiceCharge,
                DepositAmount = input.DepositAmount,
                GuestId = input.GuestId,
                StaffId = input.StaffId
            };
            await _invoiceRepository.InsertAsync(invoice);
        }

        public async Task<bool> DeleteInvoice(int id)
        {
            try
            {
                var invoice = await _invoiceRepository.FirstOrDefaultAsync(id);
                if (invoice == null)
                {
                    return false;
                }

                await _invoiceRepository.DeleteAsync(invoice);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateInvoice(InvoiceDto input)
        {
            try
            {
                var existingInvoice = await _invoiceRepository.GetAsync(input.Id);

                if (existingInvoice == null)
                {
                    return false;
                }

                existingInvoice.CreateTime = input.CreateTime;
                existingInvoice.TotalBill = input.TotalBill;
                existingInvoice.Note = input.Note;
                existingInvoice.RoomCharge = input.RoomCharge;
                existingInvoice.ServiceCharge = input.ServiceCharge;
                existingInvoice.DepositAmount = input.DepositAmount;
                existingInvoice.GuestId = input.GuestId;
                existingInvoice.StaffId = input.StaffId;

                await _invoiceRepository.UpdateAsync(existingInvoice);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<InvoiceDto>> SearchInvoices(string keyword)
        {
            try
            {
                var invoices = await _invoiceRepository.GetAllListAsync();
                var filteredInvoices = invoices
                    .Where(invoice =>
                        invoice.TotalBill.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        invoice.RoomCharge.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        invoice.ServiceCharge.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        invoice.DepositAmount.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var dtoList = filteredInvoices.Select(invoice => new InvoiceDto
                {
                    Id = invoice.Id,
                    CreateTime = invoice.CreateTime,
                    TotalBill = invoice.TotalBill,
                    Note = invoice.Note,
                    RoomCharge = invoice.RoomCharge,
                    ServiceCharge = invoice.ServiceCharge,
                    DepositAmount = invoice.DepositAmount,
                    GuestId = invoice.GuestId,
                    StaffId = invoice.StaffId
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
