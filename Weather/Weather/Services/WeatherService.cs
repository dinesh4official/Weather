using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Weather.Data;
using Weather.Helper.Interface;
using Weather.Models;
using Xamarin.Forms.Internals;

namespace Weather.Services
{
    [Preserve(AllMembers = true)]
    public class WeatherService : IWeatherService
    {
        #region Fields

        static WeatherService weatherService;

        #endregion

        #region Constructor

        private WeatherService()
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

        #region Interface Methods

        /// <summary>
        /// Gets the weather report information with respect to the city.
        /// </summary>
        /// <param name="city">Represents the name of the city.</param>
        /// <returns>Returns the weather report.</returns>
        public async Task<WeatherReport> GetWeatherReportByCity(string city)
        {
            string url = GetCityURL(city, true);
            return await GetReportAsync(url).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the weather report information with respect to the zip code.
        /// </summary>
        /// <param name="city">Represents the zip code of the city.</param>
        /// <returns>Returns the weather report.</returns>
        public async Task<WeatherReport> GetWeatherReportByPostal(int postalCode)
        {
            //If country is not specified then the search works for USA as a default.
            string url = GetPostalURL(postalCode, true);
            return await GetReportAsync(url).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the weather forecast information with respect to the city.
        /// </summary>
        /// <param name="city">Represents the name of the city.</param>
        /// <returns>Returns the weather forecast.</returns>
        public async Task<WeatherForecast> GetWeatherForecastByCity(string city)
        {
            string url = GetCityURL(city, false);
            return await GetForeCastAsync(url).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the weather forecast information with respect to the zip code.
        /// </summary>
        /// <param name="city">Represents the zip code of the city.</param>
        /// <returns>Returns the weather forecast.</returns>
        public async Task<WeatherForecast> GetWeatherForecastByPostal(int postalCode)
        {
            //If country is not specified then the search works for USA as a default.
            string url = GetPostalURL(postalCode, false);
            return await GetForeCastAsync(url).ConfigureAwait(false);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to get the city url for <see cref="WeatherReport"/> and <see cref="WeatherForecast"/>.
        /// </summary>
        /// <param name="city">Represents the name of the city.</param>
        /// <param name="isWeatherReport">Indicates whether URL should be based on weather report or weather forecast.</param>
        /// <returns>If true, returns the URL with weather report else weather forecast.</returns> 
        private string GetCityURL(string city, bool isWeatherReport)
        {
            string url = isWeatherReport ? APIConstants.OpenWeatherMapReportUrl : APIConstants.OpenWeatherMapForecastUrl;
            url += AppConstants.QueryFormat;
            url += city + AppConstants.MetricUnit;
            url += AppConstants.OpenWeatherMapAPIIDFormat + APIConstants.OpenWeatherMapAPIKey;
            return url;
        }

        /// <summary>
        /// Method to get the postal url for <see cref="WeatherReport"/> and <see cref="WeatherForecast"/>.
        /// </summary>
        /// <param name="city">Represents the zip code of the city.</param>
        /// <param name="isWeatherReport">Indicates whether URL should be based on weather report or weather forecast.</param>
        /// <returns>If true, returns the URL with weather report else weather forecast.</returns> 
        private string GetPostalURL(int postalCode, bool isWeatherReport)
        {
            string url = isWeatherReport ? APIConstants.OpenWeatherMapReportUrl : APIConstants.OpenWeatherMapForecastUrl;
            url += AppConstants.ZipFormat;
            url += postalCode + AppConstants.CountryCode + AppConstants.MetricUnit;
            url += AppConstants.OpenWeatherMapAPIIDFormat + APIConstants.OpenWeatherMapAPIKey;
            return url;
        }

        private async Task<WeatherReport> GetReportAsync(string query)
        {
            string content = await GetAPIRespose(query);
            return content == null ? null : JsonConvert.DeserializeObject<WeatherReport>(content);
        }

        private async Task<WeatherForecast> GetForeCastAsync(string query)
        {
            string content = await GetAPIRespose(query);
            return content == null ? null : JsonConvert.DeserializeObject<WeatherForecast>(content);
        }

        private async Task<string> GetAPIRespose(string query)
        {
            string content = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(query);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(AppConstants.ErrorFormat, ex.Message);
            }

            return content;
        }

        #endregion
    }
}