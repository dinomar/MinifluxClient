using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class CreateFeedResponseModel
    {
        [JsonProperty("feed_id")]
        public int FeedId { get; set; }
    }
}
