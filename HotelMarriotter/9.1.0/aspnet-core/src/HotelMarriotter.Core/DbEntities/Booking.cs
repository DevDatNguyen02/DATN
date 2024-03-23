using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public class Booking : FullAuditedEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int RegisFormId { get; set; }
        public RegisForm RegisForm { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public ICollection<BookingService> BookingService { get; set; }
    }
}
