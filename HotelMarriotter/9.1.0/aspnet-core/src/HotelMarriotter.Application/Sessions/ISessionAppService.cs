using System.Threading.Tasks;
using Abp.Application.Services;
using HotelMarriotter.Sessions.Dto;

namespace HotelMarriotter.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
