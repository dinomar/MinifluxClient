using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Miniflux.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UpdateUserRequestModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }


        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("entry_sorting_direction")]
        public string EntrySortingDirection { get; set; } // TODO enum?

        [JsonProperty("stylesheet")]
        public string Stylesheet { get; set; }

        [JsonProperty("google_id")]
        public string GoogleId { get; set; }

        [JsonProperty("openid_connect_id")]
        public string OpenidConnectId { get; set; }

        [JsonProperty("entries_per_page")]
        public int EntriesPerPage { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("keyboard_shortcuts")]
        public bool KeyboardShortcut { get; set; }

        [JsonProperty("show_reading_time")]
        public bool ShowReadingTime { get; set; }

        [JsonProperty("entry_swipe")]
        public bool EntrySwipe { get; set; }
    }
}
