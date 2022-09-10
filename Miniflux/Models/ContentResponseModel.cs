using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class ContentResponseModel
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
