using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public class Guest : FullAuditedEntity
    {
        public String Name { get; set; }
        public Gender Gender { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String CCCD { get; set; }
        public bool IsActive { get; set; }
    }
}
