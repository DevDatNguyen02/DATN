using System.Threading.Tasks;
using HotelMarriotter.Configuration.Dto;

namespace HotelMarriotter.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
