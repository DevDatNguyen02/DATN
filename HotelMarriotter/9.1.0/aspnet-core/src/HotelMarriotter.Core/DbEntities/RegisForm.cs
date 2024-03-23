using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public class RegisForm : FullAuditedEntity
    {
        public DateTime DateRegis { get; set; }
        public float DepositAmount { get; set; }
        public int NumberOfPeople { get; set; }
        public string Note { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Service> Service { get; set; }
    }
}
