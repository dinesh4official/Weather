using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace Weather.Models
{
    [Preserve(AllMembers = true)]
    public class CityInfo
    {
        #region Constructor

        public CityInfo()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the City ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the City Name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the City co-ordinates.
        /// </summary>
        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        /// <summary>
        /// Gets or sets the state of the city.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        #endregion
    }
}
