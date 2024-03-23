using HotelMarriotter.Debugging;

namespace HotelMarriotter
{
    public class HotelMarriotterConsts
    {
        public const string LocalizationSourceName = "HotelMarriotter";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "e3158ac5b62d42aeaac046f3eaa6ddf2";
    }
}
