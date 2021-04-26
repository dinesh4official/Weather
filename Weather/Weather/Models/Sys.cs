using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace Weather.Models
{
    [Preserve(AllMembers = true)]
    public class Sys
    {
        #region Constructor

        public Sys()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Internal parameter.
        /// </summary>
        [JsonProperty("type")]
        public long Type { get; set; }

        /// <summary>
        /// Internal parameter.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Internal parameter.
        /// </summary>
        [JsonProperty("message")]
        public double Message { get; set; }

        /// <summary>
        /// Country code (GB, JP etc.).
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Sunrise time, unix, UTC.
        /// </summary>
        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        /// <summary>
        /// Sunset time, unix, UTC.
        /// </summary>
        [JsonProperty("sunset")]
        public long Sunset { get; set; }

        #endregion
    }
}
