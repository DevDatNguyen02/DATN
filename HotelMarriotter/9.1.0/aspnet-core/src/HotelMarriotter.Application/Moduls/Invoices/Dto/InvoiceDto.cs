using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.Invoices.Dto
{
    public class InvoiceDto:EntityDto
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public float TotalBill { get; set; }
        public string Note { get; set; }
        public float RoomCharge { get; set; }
        public float ServiceCharge { get; set; }
        public float DepositAmount { get; set; }
        public int GuestId { get; set; }
        public int StaffId { get; set; }
    }
}
