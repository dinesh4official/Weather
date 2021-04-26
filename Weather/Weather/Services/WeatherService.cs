using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Weather.Data;
using Weather.Models;
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

        #region Internal Methods

        public async Task<WeatherReport> GetWeatherReportByCity(string city)
        {
            string url = APIConstants.OpenWeatherMapBaseUrl;
            url += AppConstants.QueryFormat;
            url += city + AppConstants.ImperialUnit;
            url += AppConstants.OpenWeatherMapAPIIDFormat + APIConstants.OpenWeatherMapAPIKey;
            return await GetReportAsync(url).ConfigureAwait(false);
        }

        public async Task<WeatherReport> GetWeatherReportByPostal(string postalCode)
        {
            //If country is not specified then the search works for USA as a default.
            string url = APIConstants.OpenWeatherMapBaseUrl;
            url += AppConstants.ZipFormat;
            url += postalCode + AppConstants.ImperialUnit;
            url += AppConstants.OpenWeatherMapAPIIDFormat + APIConstants.OpenWeatherMapAPIKey;
            return await GetReportAsync(url).ConfigureAwait(false);
        }

        #endregion

        #region Private Methods

        private async Task<WeatherReport> GetReportAsync(string query)
        {
            WeatherReport weatherReport = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(query);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        weatherReport = JsonConvert.DeserializeObject<WeatherReport>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(AppConstants.ErrorFormat, ex.Message);
            }

            return weatherReport;
        }

        #endregion
    }
}