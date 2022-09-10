using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class CountersResponseModel
    {
        [JsonProperty("reads")]
        public Dictionary<string, int> Reads { get; set; }

        [JsonProperty("unreads")]
        public Dictionary<string, int> Unreads { get; set; }
    }
}
