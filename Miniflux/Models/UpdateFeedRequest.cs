using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class UpdateFeedRequest
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }


        [JsonProperty("feed_url")]
        public string FeedUrl { get; set; }

        [JsonProperty("site_url")]
        public string SiteUrl { get; set; }

        [JsonProperty("scraper_rules")]
        public string ScraperRules { get; set; }

        [JsonProperty("rewrite_rules")]
        public string RewriteRules { get; set; }

        [JsonProperty("blocklist_rules")]
        public string BlocklistRules { get; set; }

        [JsonProperty("keeplist_rules")]
        public string KeeplistRules { get; set; }

        [JsonProperty("crawler")]
        public bool Crawler { get; set; }

        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("ignore_http_cache")]
        public bool IgnoreHttpCache { get; set; }

        [JsonProperty("fetch_via_proxy")]
        public bool FetchViaProxy { get; set; }
    }
}
