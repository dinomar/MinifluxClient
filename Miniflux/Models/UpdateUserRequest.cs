using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class UpdateUserRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
