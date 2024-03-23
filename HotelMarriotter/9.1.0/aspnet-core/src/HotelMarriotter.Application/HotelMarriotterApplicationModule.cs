using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HotelMarriotter.Authorization;

namespace HotelMarriotter
{
    [DependsOn(
        typeof(HotelMarriotterCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class HotelMarriotterApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<HotelMarriotterAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(HotelMarriotterApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
