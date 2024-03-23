using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public class Invoice : FullAuditedEntity
    {
        public DateTime CreateTime { get; set; }
        public float TotalBill { get; set; }
        public string Note { get; set; }
        public float RoomCharge { get; set; } // Tiền phòng
        public float ServiceCharge { get; set; } // Tiền dịch vụ
        public float DepositAmount { get; set; } // Tiền đặt cọc

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public ICollection<Service> Service { get; set; }
        public ICollection<BookingService> BookingService { get; set; }
    }
}
