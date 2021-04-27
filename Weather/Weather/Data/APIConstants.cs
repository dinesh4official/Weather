using System;
using Xamarin.Forms.Internals;

namespace Weather.Data
{
    [Preserve(AllMembers = true)]
    public static class APIConstants
    {
        public static string OpenWeatherMapAPIKey = "773da403e537538d1457d960a59fe953";
        public static string OpenWeatherMapReportUrl = "https://api.openweathermap.org/data/2.5/weather";
        public static string OpenWeatherMapForecastUrl = "https://api.openweathermap.org/data/2.5/forecast";
    }
}
