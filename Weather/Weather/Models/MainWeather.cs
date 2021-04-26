using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;
namespace Weather.Models
{
    [Preserve(AllMembers = true)]
    public class MainWeather
    {
        #region Constructor

        public MainWeather()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Temperature.
        /// </summary>
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        /// <summary>
        /// Atmospheric pressure.
        /// </summary>
        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        /// <summary>
        /// Humidity.
        /// </summary>
        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        /// <summary>
        /// Minimum temperature at the moment. This is minimal currently observed temperature (within large megalopolises and urban areas).
        /// </summary>
        [JsonProperty("temp_min")]
        public double MinTemperature { get; set; }

        /// <summary>
        /// Maximum temperature at the moment. This is maximal currently observed temperature (within large megalopolises and urban areas).
        /// </summary>
        [JsonProperty("temp_max")]
        public double MaxTemperature { get; set; }

        #endregion
    }
}