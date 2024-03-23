using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.DbEntities
{
    public class Room : FullAuditedEntity
    {
        public string NameRoom { get; set; }
        public string Situation { get; set; }
        public string ImageUrl { get; set; }
        public int TypeRoomId { get; set; }
        public TypeRoom TypeRooms { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Booking> Booking { get; set; }
    }
}
