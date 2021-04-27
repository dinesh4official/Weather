using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace Weather.Models
{
    [Preserve(AllMembers = true)]
    public class WeatherForecast
    {
        #region Constructor

        public WeatherForecast()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Internal parameter.
        /// </summary>
        [JsonProperty("cod")]
        public string Cod { get; set; }

        /// <summary>
        /// Internal parameter.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// A number of timestamps returned in the API response.
        /// </summary>
        [JsonProperty("cnt")]
        public int Cnt { get; set; }

        /// <summary>
        /// Gets or sets the weather forecast information.
        /// </summary>
        [JsonProperty("list")]
        public List<WeatherReport> WeatherReport { get; set; } = new List<WeatherReport>();

        #endregion
    }
}
