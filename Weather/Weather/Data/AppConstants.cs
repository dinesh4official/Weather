using System;
using Xamarin.Forms.Internals;

namespace Weather.Data
{
    [Preserve(AllMembers = true)]
    public static class AppConstants
    {

        public const string SearchBarPosition = "SearchBarPosition";
        public const string BindableError = "Invalid bindable object is assigned";
        public const string AvailableConnection = "Network Connected";
        public const string NoConnection = "No Network Connection";
        public const string ErrorFormat = "\t\tError {0}";
        public const string ZipFormat = "?zip=";
        public const string QueryFormat = "?q=";
        public const string OpenWeatherMapAPIIDFormat = "&appid=";
        public const string ImperialUnit = "&units=imperial";
        public const string CityJSONFilePath = "Resources.citylist.json";
    }
}
