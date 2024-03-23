using Abp.Application.Services;
using HotelMarriotter.MultiTenancy.Dto;

namespace HotelMarriotter.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

