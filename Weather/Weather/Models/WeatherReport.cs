using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace Weather.Models
{
    [Preserve(AllMembers = true)]
    public class WeatherReport : NotifyListener
    {
        #region Fields

        bool isFavourie;

        #endregion

        #region Constructor

        public WeatherReport()
        {
           
        }

        #endregion

        #region Properties

        /// <summary>
        /// City ID.
        /// </summary>
        [JsonProperty("id")]
        public long CityId { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        [JsonProperty("name")]
        public string CityName { get; set; }

        /// <summary>
        /// More information about the weather.
        /// </summary>
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; } = new List<Weather>();

        /// <summary>
        /// Time of data calculation, unix, UTC.
        /// </summary>
        [JsonProperty("dt")]
        public long Date { get; set; }

        /// <summary>
        /// Contains information like temperature, pressure etc..,
        /// </summary>
        [JsonProperty("main")]
        public MainWeather MainWeather { get; set; } = new MainWeather();

        /// <summary>
        /// Contains information like sun rise, sun set etc..,
        /// </summary>
        [JsonProperty("sys")]
        public Sys System { get; set; } = new Sys();

        public bool IsFavourie
        {
            get => isFavourie;
            set
            {
                isFavourie = value;
                OnPropertyChanged(nameof(IsFavourie));
            }
        }

        #endregion
    }
}