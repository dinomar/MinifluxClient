using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class UpdateEntriesRequestModel
    {
        [JsonProperty("entry_ids")]
        public int[] EntryIds { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
