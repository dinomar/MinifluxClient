using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class ClientResponseModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
