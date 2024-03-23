using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.Services.Dto
{
    public class ServiceDto:EntityDto
    {
        public string ServiceName { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
      /*  public IFormFile ImageFile { get; set; }*/
        public bool IsActive { get; set; }
    }
}
