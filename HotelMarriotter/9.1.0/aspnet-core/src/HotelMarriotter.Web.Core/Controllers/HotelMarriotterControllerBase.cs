using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace HotelMarriotter.Controllers
{
    public abstract class HotelMarriotterControllerBase: AbpController
    {
        protected HotelMarriotterControllerBase()
        {
            LocalizationSourceName = HotelMarriotterConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
