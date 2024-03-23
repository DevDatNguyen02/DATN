using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public class Service : FullAuditedEntity
    {
        public string ServiceName { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public ICollection<RegisForm> RegisFormServices { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
