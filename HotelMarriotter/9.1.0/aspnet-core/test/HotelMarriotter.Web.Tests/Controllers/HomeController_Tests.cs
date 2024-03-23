using System.Threading.Tasks;
using HotelMarriotter.Models.TokenAuth;
using HotelMarriotter.Web.Controllers;
using Shouldly;
using Xunit;

namespace HotelMarriotter.Web.Tests.Controllers
{
    public class HomeController_Tests: HotelMarriotterWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}