using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace Weather.Models
{
    [Preserve(AllMembers = true)]
    public class Weather
    {
        #region Constructor

        public Weather()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Weather condition id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// Group of weather parameters (Rain, Snow, Extreme etc.).
        /// </summary>
        [JsonProperty("main")]
        public string Main { get; set; } = string.Empty;

        /// <summary>
        /// Weather condition within the group. You can get the output in your language.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Weather icon id.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; } = string.Empty;

        #endregion
    }
}