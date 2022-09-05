using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class ClientResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
