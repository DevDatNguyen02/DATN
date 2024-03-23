using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public class BookingService : FullAuditedEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int Quantity { get; set; } // Số lượng dịch vụ được yêu cầu
        public float Price { get; set; } // Giá dịch vụ
        public float TotalAmount { get; set; } // Tổng giá trị thanh toán cho dịch vụ

        public DateTime RequestedDate { get; set; } // Ngày yêu cầu dịch vụ
        public DateTime? ConfirmedDate { get; set; } // Ngày xác nhận dịch vụ
        public bool IsConfirmed { get; set; } // Trạng thái xác nhận dịch vụ
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

    }
}
