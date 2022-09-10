using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Miniflux.Models
{
    public class CreateUpdateCategoryRequestModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
