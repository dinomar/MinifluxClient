using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class DiscoverRequest
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
