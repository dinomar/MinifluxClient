using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class ErrorResponseModel
    {
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
