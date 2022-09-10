using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class EntriesResponseModel
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("entries")]
        public IEnumerable<Entry> Entries { get; set; }
    }
}
