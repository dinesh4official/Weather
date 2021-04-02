using System;
using System.Net.Http;
using Xamarin.Forms.Internals;

namespace Weather.Services
{
    [Preserve(AllMembers = true)]
    public class WeatherService
    {
        #region Fields

        static WeatherService weatherService;

        #endregion

        #region Constructor

        public WeatherService()
        {

        }

        #endregion

        #region Static Property

        public static WeatherService Instance
        {
            get
            {
                if (weatherService == null)
                    weatherService = new WeatherService();

                return weatherService;
            }
        }

        #endregion
    }
}
