using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HotelMarriotter.Configuration;

namespace HotelMarriotter.Web.Host.Startup
{
    [DependsOn(
       typeof(HotelMarriotterWebCoreModule))]
    public class HotelMarriotterWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public HotelMarriotterWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HotelMarriotterWebHostModule).GetAssembly());
        }
    }
}
