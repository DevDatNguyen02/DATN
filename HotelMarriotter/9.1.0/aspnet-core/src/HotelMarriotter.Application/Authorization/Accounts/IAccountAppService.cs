using System.Threading.Tasks;
using Abp.Application.Services;
using HotelMarriotter.Authorization.Accounts.Dto;

namespace HotelMarriotter.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
