using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HotelMarriotter.Configuration;
using HotelMarriotter.EntityFrameworkCore;
using HotelMarriotter.Migrator.DependencyInjection;

namespace HotelMarriotter.Migrator
{
    [DependsOn(typeof(HotelMarriotterEntityFrameworkModule))]
    public class HotelMarriotterMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public HotelMarriotterMigratorModule(HotelMarriotterEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(HotelMarriotterMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                HotelMarriotterConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HotelMarriotterMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
