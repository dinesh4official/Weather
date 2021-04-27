using System;
using Xamarin.Forms.Internals;

namespace Weather.Data
{
    [Preserve(AllMembers = true)]
    public static class AppConstants
    {
        public const string BindableError = "Invalid bindable object is assigned";
        public const string AvailableConnection = "Network Connected";
        public const string NoConnection = "No Network Connection";
        public const string UnableTogetData = "Unable to get the information. Please check either the network connection or input data";
        public const string ErrorFormat = "\t\tError {0}";
        public const string ZipFormat = "?zip=";
        public const string QueryFormat = "?q=";
        public const string OpenWeatherMapAPIIDFormat = "&appid=";
        public const string ImperialUnit = "&units=imperial";
        public const string CityJSONFilePath = "Resources.citylist.json";
        public const string BooleanTarget = "The target should be a boolean";
        public const string LongTarget = "The target should be a long";
        public const string UTC = "UTC";
        public const string DataLoadingMessage = "Please wait while we fetch the data";
        public const string SearchTextEmpty = "Please enter the valid data";
    }
}
