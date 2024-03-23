using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace HotelMarriotter.Localization
{
    public static class HotelMarriotterLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(HotelMarriotterConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(HotelMarriotterLocalizationConfigurer).GetAssembly(),
                        "HotelMarriotter.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
