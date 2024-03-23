using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HotelMarriotter.EntityFrameworkCore;
using HotelMarriotter.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace HotelMarriotter.Web.Tests
{
    [DependsOn(
        typeof(HotelMarriotterWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class HotelMarriotterWebTestModule : AbpModule
    {
        public HotelMarriotterWebTestModule(HotelMarriotterEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HotelMarriotterWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(HotelMarriotterWebMvcModule).Assembly);
        }
    }
}