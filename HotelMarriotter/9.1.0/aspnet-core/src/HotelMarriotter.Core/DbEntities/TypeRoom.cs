using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public class TypeRoom : FullAuditedEntity
    {
        public string NameTypeRoom { get; set; }
        public float Price { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public bool IsActive { get; set; }
    }
}
