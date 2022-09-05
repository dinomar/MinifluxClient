using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class Feed
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("site_url")]
        public string SiteUrl { get; set; }

        [JsonProperty("feed_url")]
        public string FeedUrl { get; set; }

        [JsonProperty("checked_at")]
        public string CheckedAt { get; set; }

        [JsonProperty("etag_header")]
        public string EtagHeader { get; set; }

        [JsonProperty("last_modified_header")]
        public string LastModifiedHeader { get; set; }

        [JsonProperty("parsing_error_message")]
        public string ParsingErrorMessage { get; set; }

        [JsonProperty("parsing_error_count")]
        public int ParsingErrorCount { get; set; }

        [JsonProperty("scraper_rules")]
        public string ScraperRules { get; set; }

        [JsonProperty("rewrite_rules")]
        public string RewriteRules { get; set; }

        [JsonProperty("crawler")]
        public bool Crawler { get; set; }

        [JsonProperty("blocklist_rules")]
        public string BlocklistRules { get; set; }

        [JsonProperty("keeplist_rules")]
        public string KeeplistRules { get; set; }

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

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("icon")]
        public FeedIcon Icon { get; set; }
    }
}
