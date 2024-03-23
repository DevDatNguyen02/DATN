using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public class BookingRegisForm : FullAuditedEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int RegisFormId { get; set; }
        public RegisForm RegisForm { get; set; }
    }
}
