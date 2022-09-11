# Miniflux .Net Core API Client
.Net Core 3.1 client library for Miniflux API.

## What is Miniflux?
[Miniflux](https://github.com/miniflux/v2) is a minimalist and opinionated feed reader.

## Tests
Some tests should be edited and supplied with a Miniflux host address and it's credentials to run properly.
```cs
private static readonly string _minifluxUrl = "http://192.168.1.100";
private static readonly string _minifluxUsername = "admin";
private static readonly string _minifluxPassword = "admin";
private static readonly string _minifluxApiKey = "";
```

## Example
```cs
// Creating a client using username/password authentication.
MinifluxClient client = new MinifluxClient("https://miniflux.example.org", "myusername", "mypassword");

// Creating a client using API Key authentication.
MinifluxClient client = new MinifluxClient("https://miniflux.example.org", apiKey: "My secret API token");

// Get all feeds.
var feeds = await client.GetFeedsAsync();

// Refresh a feed with is 42.
await client.RefreshFeedAsync(42);

// Discover subscriptions from a website.
var subscriptions = await client.DiscoverAsync("https://example.org");

// Create a new feed, with a personalized user agent and with the crawler enabled.
int feedId = await client.CreateFeedAsync("http://example.org/feed.xml", 12, crawler: true, userAgent: "GoogleBot");

// Fetch 10 starred entries.
var entries = await client.GetEntriesAsync(starred: true, limit: 10);

// Fetch last 5 feed entries.
var feedEntries = await client.GetFeedEntriesAsync(123, direction: DirectionFilter.Desc, order: OrderFilter.Published_At, limit: 5);

// Fetch entries that belongs to a category with status unread and read.
var entries = await client.GetEntriesAsync(categoryId: 456, status: StatusFilter.Read | StatusFilter.Unread);

// Update a feed category.
await client.UpdateFeedAsync(123, categoryId: 456);

// OPML Export
string opml = await client.ExportAsync();

// Get application version.
string version = await client.VersionAsync();

// Exception/Error handling.
try
{
    MinifluxClient client2 = new MinifluxClient("https://miniflux.example.org", apiKey: "My secret API token");
    string status = await client2.HealthCheckAsync();
}
catch (ClientException ex)
{
    Console.WriteLine($"Client error: {ex.StatusCode} {ex.Message}");
}
catch (System.Net.WebException ex) {
    Console.WriteLine($"Network error: {ex.Message}");
}
```
