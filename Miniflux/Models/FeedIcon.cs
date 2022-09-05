using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class FeedIcon
    {
        [JsonProperty("feed_id")]
        public int FeedId { get; set; }

        [JsonProperty("icon_id")]
        public int IconId { get; set; }
    }
}
