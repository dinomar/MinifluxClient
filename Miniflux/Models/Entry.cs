using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public class Entry
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("feed_id")]
        public int FeedId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("comments_url")]
        public string CommentsUrl { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("published_at")]
        public string PublichedAt { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("share_code")]
        public string ShareCode { get; set; }

        [JsonProperty("starred")]
        public bool Starred { get; set; }

        [JsonProperty("reading_time")]
        public int ReadingTime { get; set; }

        //[JsonProperty("enclosures")]
        //public string Enclosures { get; set; }

        [JsonProperty("feed")]
        public Feed Feed { get; set; }
    }
}
