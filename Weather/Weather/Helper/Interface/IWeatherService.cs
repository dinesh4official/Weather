using System;
using System.Threading.Tasks;
using Weather.Models;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Interface
{
    [Preserve(AllMembers = true)]
    public interface IWeatherService
    {
        Task<WeatherReport> GetWeatherReportByCity(string city);

        Task<WeatherReport> GetWeatherReportByPostal(int postalCode);

        Task<WeatherForecast> GetWeatherForecastByCity(string city);

        Task<WeatherForecast> GetWeatherForecastByPostal(int postalCode);
    }
}
