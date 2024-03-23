using Abp.Application.Services.Dto;

namespace HotelMarriotter.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

