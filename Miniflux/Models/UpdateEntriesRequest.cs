using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class UpdateEntriesRequest
    {
        [JsonProperty("entry_ids")]
        public int[] entryIds { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
