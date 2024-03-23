using Abp.Application.Services.Dto;
using HotelMarriotter.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.TypeRooms.Dto
{
    public class TypeRoomDto:EntityDto
    {
        public string NameTypeRoom { get; set; }
        public float Price { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
