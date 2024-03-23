using Abp.Application.Services.Dto;
using HotelMarriotter.DbEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.Rooms.Dto
{
    public class RoomDto:EntityDto
    {
        public string NameRoom { get; set; }
        public string Situation { get; set; }
        public string NameTypeRoom { get; set; }
        public string ImageUrl { get; set; }
       /* public IFormFile ImageFile { get; set; }*/
        public int TypeRoomId { get; set; }
        public TypeRoom TypeRooms { get; set; }
        public bool IsActive { get; set; }
    }
}
