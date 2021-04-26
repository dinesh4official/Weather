using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace Weather.Models
{
    [Preserve(AllMembers = true)]
    public class Coord
    {
        /// <summary>
        /// Gets or sets the longitude of the city.
        /// </summary>
        [JsonProperty("lon")]
        public double Longitude { get; set; } = 0;

        /// <summary>
        /// Gets or sets the latitude of the city.
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; set; } = 0;
    }
}
