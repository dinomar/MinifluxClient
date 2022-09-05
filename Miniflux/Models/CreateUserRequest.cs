using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class CreateUserRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }


        [JsonProperty("google_id")]
        public string GoogleId { get; set; }

        [JsonProperty("openid_connect_id")]
        public string OpenidConnectId { get; set; }
    }
}
