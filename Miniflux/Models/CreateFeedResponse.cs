using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class CreateFeedResponse
    {
        [JsonProperty("feed_id")]
        public int FeedId { get; set; }
    }
}
