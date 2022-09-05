using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class Icon
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
    }
}
