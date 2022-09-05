using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class ErrorResponse
    {
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
