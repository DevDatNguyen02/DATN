using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMarriotter.Moduls.RegisForms.Dto
{
    public class RegisFormDto:EntityDto
    {
        public int Id { get; set; }
        public DateTime DateRegis { get; set; }
        public float DepositAmount { get; set; }
        public int NumberOfPeople { get; set; }
        public string Note { get; set; }
        public int GuestId { get; set; }
        public int StaffId { get; set; }
    }
}
