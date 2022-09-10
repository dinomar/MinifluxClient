using Miniflux.Models;
using Miniflux.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Miniflux.Clients
{
    public class MinifluxClient : BaseRESTClient
    {
        public static readonly int APIVERSION = 1;


        public MinifluxClient(string baseUrl, string username, string password, int timeout = 30)
            : base(baseUrl, timeout)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Headers.Add("Authorization", "Basic " + ToBase64String($"{username}:{password}"));
            }
        }

        public MinifluxClient(string baseUrl, string apiKey=null, int timeout = 30, Dictionary<string, string> headers = null)
            : base(baseUrl, timeout, headers)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            if (!string.IsNullOrEmpty(apiKey))
            {
                Headers.Add("X-Auth-Token", apiKey);
            }
        }


        public async Task<IEnumerable<Subscription>> DiscoverAsync(
            string url,
            string username="",
            string password="",
            string userAgent = "",
            bool fetchViaProxy = false)
        {
            return await DiscoverAsync(new DiscoverRequestModel
            {
                Url = url,
                Username = username,
                Password = password,
                UserAgent = userAgent,
                FetchViaProxy = fetchViaProxy
            });
        }

        public async Task<IEnumerable<Subscription>> DiscoverAsync(DiscoverRequestModel discoverRequest)
        {
            string json = JsonConvert.SerializeObject(discoverRequest);

            return await PostAsync<IEnumerable<Subscription>>($"/v{APIVERSION}/discover", json);
        }

        public async Task<IEnumerable<Feed>> GetFeedsAsync()
        {
            return await GetAsync<IEnumerable<Feed>>($"/v{APIVERSION}/feeds");
        }

        public async Task<Feed> GetFeedAsync(int id)
        {
            return await GetAsync<Feed>($"/v{APIVERSION}/feeds/{id}");
        }

        public async Task<Icon> GetFeedIconAsync(int id)
        {
            return await GetAsync<Icon>($"/v{APIVERSION}/feeds/{id}/icon");
        }

        public async Task<CreateFeedResponseModel> CreateFeedAsync(
            string feedUrl,
            int categoryId,
            string username="",
            string password="",
            bool crawler=false,
            string userAgent="",
            string scraperRules="",
            string rewriteRules="",
            string blocklistRules="",
            string keeplistRules="",
            bool disabled=false,
            bool ignoreHttpCache=false,
            bool fetchViaProxy=false)
        {
            return await CreateFeedAsync(new CreateFeedRequestModel
            {
                FeedUrl = feedUrl,
                CategoryId = categoryId,
                Username = username,
                Password = password,
                Crawler = crawler,
                UserAgent = userAgent,
                ScraperRules = scraperRules,
                RewriteRules = rewriteRules,
                BlocklistRules = blocklistRules,
                KeeplistRules = keeplistRules,
                Disabled = disabled,
                IgnoreHttpCache = ignoreHttpCache,
                FetchViaProxy = fetchViaProxy
            });
        }

        public async Task<CreateFeedResponseModel> CreateFeedAsync(CreateFeedRequestModel createFeed)
        {
            string json = JsonConvert.SerializeObject(createFeed);

            return await PostAsync<CreateFeedResponseModel>($"/v{APIVERSION}/feeds", json);
        }

        public async Task<Feed> UpdateFeedAsync(
            int feedId,
            string title,
            int categoryId,
            string feedUrl=null,
            string siteUrl=null,
            string scraperRules = null,
            string rewriteRules = null,
            string blocklistRules = null,
            bool? crawler = null,
            string userAgent = null,
            string username = null,
            string password = null,
            bool? ignoreHttpCache = null,
            bool? fetchViaProxy = null)
        {
            return await UpdateFeedAsync(feedId, new UpdateFeedRequestModel
            {
                Title = title,
                CategoryId = categoryId,
                FeedUrl = feedUrl,
                SiteUrl = siteUrl,
                ScraperRules = scraperRules,
                RewriteRules = rewriteRules,
                BlocklistRules = blocklistRules,
                Crawler = crawler,
                UserAgent = userAgent,
                Username = username,
                Password = password,
                IgnoreHttpCache = ignoreHttpCache,
                FetchViaProxy = fetchViaProxy
            });
        }

        public async Task<Feed> UpdateFeedAsync(int feedId, UpdateFeedRequestModel updateFeed)
        {
            string json = JsonConvert.SerializeObject(updateFeed);

            return await PutAsync<Feed>($"/v{APIVERSION}/feeds/{feedId}", json);
        }

        public async Task DeleteFeedAsync(int feedId)
        {
            await DeleteAsync($"/v{APIVERSION}/feeds/{feedId}");
        }

        public async Task RefreshFeedAsync(int feedId)
        {
            await PutAsync($"/v{APIVERSION}/feeds/{feedId}/refresh");
        }

        public async Task RefreshAllFeedsAsync()
        {
            await PutAsync($"/v{APIVERSION}/feeds/refresh");
        }

        public async Task<Entry> GetFeedEntryAsync(int feedId, int entryId)
        {
            return await GetAsync<Entry>($"/v{APIVERSION}/feeds/{feedId}/entries/{entryId}");
        }

        public async Task<ContentResponseModel> FetchOriginalArticleAsync(int entryId)
        {
            return await GetAsync<ContentResponseModel>($"/v{APIVERSION}/entries/{entryId}/fetch-content");
        }

        public async Task<EntriesResponseModel> GetFeedEntriesAsync(
            int feedId,
            StatusFilter? status=null,
            int? offset=null,
            int? limit=null,
            OrderFilter? order=null,
            DirectionFilter? direction=null,
            int? before=null,
            int? after=null,
            Int64? beforeEntryId=null,
            Int64? afterEntryId=null,
            bool? starred=null,
            string search=null,
            int? categoryId=null)
        {
            StringBuilder sb = new StringBuilder("?");

            if (status != null) { sb.Append($"{nameof(status)}={Enum.GetName(typeof(StatusFilter), status)}&"); }
            if (offset != null) { sb.Append($"{nameof(offset)}={offset}&"); }
            if (limit != null) { sb.Append($"{nameof(limit)}={limit}&"); }
            if (order != null) { sb.Append($"{nameof(order)}={Enum.GetName(typeof(OrderFilter), order)}&"); }
            if (direction != null) { sb.Append($"{nameof(direction)}={Enum.GetName(typeof(DirectionFilter), direction)}&"); }
            if (before != null) { sb.Append($"{nameof(before)}={before}&"); }
            if (after != null) { sb.Append($"{nameof(after)}={after}&"); }
            if (beforeEntryId != null) { sb.Append($"before_entry_id={beforeEntryId}&"); }
            if (afterEntryId != null) { sb.Append($"after_entry_id={afterEntryId}&"); }
            if (starred != null) { sb.Append($"{nameof(starred)}={starred}&"); }
            if (search != null) { sb.Append($"{nameof(search)}={search}&"); }
            if (categoryId != null) { sb.Append($"category_id={categoryId}&"); }

            string queryString = (sb.Length > 1) ? sb.ToString(0, sb.Length - 1) : string.Empty;

            return await GetAsync<EntriesResponseModel>($"/v{APIVERSION}/feeds/{feedId}/entries" + queryString);
        }

        public async Task MarkFeedEntriesAsRead(int feedId)
        {
            await PutAsync($"/v{APIVERSION}/feeds/{feedId}/mark-all-as-read");
        }

        public async Task<Entry> GetEntryAsync(int entryId)
        {
            return await GetAsync<Entry>($"/v{APIVERSION}/entries/{entryId}");
        }

        public async Task<EntriesResponseModel> GetEntriesAsync(
            StatusFilter? status = null,
            int? offset = null,
            int? limit = null,
            OrderFilter? order = null,
            DirectionFilter? direction = null,
            int? before = null,
            int? after = null,
            Int64? beforeEntryId = null,
            Int64? afterEntryId = null,
            bool? starred = null,
            string search = null,
            int? categoryId = null)
        {
            StringBuilder sb = new StringBuilder("?");

            if (status != null) { sb.Append($"{nameof(status)}={Enum.GetName(typeof(StatusFilter), status)}&"); }
            if (offset != null) { sb.Append($"{nameof(offset)}={offset}&"); }
            if (limit != null) { sb.Append($"{nameof(limit)}={limit}&"); }
            if (order != null) { sb.Append($"{nameof(order)}={Enum.GetName(typeof(OrderFilter), order)}&"); }
            if (direction != null) { sb.Append($"{nameof(direction)}={Enum.GetName(typeof(DirectionFilter), direction)}&"); }
            if (before != null) { sb.Append($"{nameof(before)}={before}&"); }
            if (after != null) { sb.Append($"{nameof(after)}={after}&"); }
            if (beforeEntryId != null) { sb.Append($"before_entry_id={beforeEntryId}&"); }
            if (afterEntryId != null) { sb.Append($"after_entry_id={afterEntryId}&"); }
            if (starred != null) { sb.Append($"{nameof(starred)}={starred}&"); }
            if (search != null) { sb.Append($"{nameof(search)}={search}&"); }
            if (categoryId != null) { sb.Append($"category_id={categoryId}&"); }

            string queryString = (sb.Length > 1) ? sb.ToString(0, sb.Length - 1) : string.Empty;

            return await GetAsync<EntriesResponseModel>($"/v{APIVERSION}/entries" + queryString);
        }

        public async Task UpdateEntriesAsync(int[] entryIds, string status)
        {
            await UpdateEntriesAsync(new UpdateEntriesRequestModel
            {
                EntryIds = entryIds,
                Status = status
            });
        }

        public async Task UpdateEntriesAsync(UpdateEntriesRequestModel updateEntries)
        {
            string json = JsonConvert.SerializeObject(updateEntries);

            await PutAsync($"/v{APIVERSION}/entries", json);
        }

        public async Task ToggleBookmarkedAsync(int entryId)
        {
            await PutAsync($"/v{APIVERSION}/entries/{entryId}/bookmark");
        }


        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await GetAsync<IEnumerable<Category>>($"/v{APIVERSION}/categories");
        }

        public async Task<EntriesResponseModel> GetCategoryEntriesAsync(
            int categoryId,
            StatusFilter? status = null,
            int? offset = null,
            int? limit = null,
            OrderFilter? order = null,
            DirectionFilter? direction = null,
            int? before = null,
            int? after = null,
            Int64? beforeEntryId = null,
            Int64? afterEntryId = null,
            bool? starred = null,
            string search = null)
        {
            StringBuilder sb = new StringBuilder("?");

            if (status != null) { sb.Append($"{nameof(status)}={Enum.GetName(typeof(StatusFilter), status)}&"); }
            if (offset != null) { sb.Append($"{nameof(offset)}={offset}&"); }
            if (limit != null) { sb.Append($"{nameof(limit)}={limit}&"); }
            if (order != null) { sb.Append($"{nameof(order)}={Enum.GetName(typeof(OrderFilter), order)}&"); }
            if (direction != null) { sb.Append($"{nameof(direction)}={Enum.GetName(typeof(DirectionFilter), direction)}&"); }
            if (before != null) { sb.Append($"{nameof(before)}={before}&"); }
            if (after != null) { sb.Append($"{nameof(after)}={after}&"); }
            if (beforeEntryId != null) { sb.Append($"before_entry_id={beforeEntryId}&"); }
            if (afterEntryId != null) { sb.Append($"after_entry_id={afterEntryId}&"); }
            if (starred != null) { sb.Append($"{nameof(starred)}={starred}&"); }
            if (search != null) { sb.Append($"{nameof(search)}={search}&"); }

            string queryString = (sb.Length > 1) ? sb.ToString(0, sb.Length - 1) : string.Empty;

            return await GetAsync<EntriesResponseModel>($"/v{APIVERSION}/categories/{categoryId}/entries" + queryString);
        }

        public async Task<Category> CreateCategoryAsync(string title)
        {
            return await CreateCategoryAsync(new CreateUpdateCategoryRequestModel
            {
                Title = title
            });
        }

        public async Task<Category> CreateCategoryAsync(CreateUpdateCategoryRequestModel createCategory)
        {
            string json = JsonConvert.SerializeObject(createCategory);

            return await PostAsync<Category>($"/v{APIVERSION}/categories", json);
        }

        public async Task<Category> UpdateCategoryAsync(int categoryId, string newTitle)
        {
            return await UpdateCategoryAsync(categoryId, new CreateUpdateCategoryRequestModel
            {
                Title = newTitle
            });
        }

        public async Task<Category> UpdateCategoryAsync(int categoryId, CreateUpdateCategoryRequestModel updateCategory)
        {
            string json = JsonConvert.SerializeObject(updateCategory);

            return await PutAsync<Category>($"/v{APIVERSION}/categories/{categoryId}", json);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await DeleteAsync($"/v{APIVERSION}/categories/{categoryId}");
        }

        public async Task MarkCategoryEntriesAsReadAsync(int categoryId)
        {
            await PutAsync($"/v{APIVERSION}/categories/{categoryId}/mark-all-as-read");
        }


        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await GetAsync<IEnumerable<User>>($"/v{APIVERSION}/users");
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await GetAsync<User>($"/v{APIVERSION}/users/{userId}");
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await GetAsync<User>($"/v{APIVERSION}/users/{username}");
        }

        public async Task<User> CreateUserAsync(string username, string password, bool isAdmin, string googleId="", string openidConnectId="")
        {
            return await CreateUserAsync(new CreateUserRequestModel
            {
                Username = username,
                Password = password,
                IsAdmin = isAdmin,
                GoogleId = googleId,
                OpenidConnectId = openidConnectId
            });
        }

        public async Task<User> CreateUserAsync(CreateUserRequestModel createUser)
        {
            string json = JsonConvert.SerializeObject(createUser);

            return await PostAsync<User>($"/v{APIVERSION}/users", json);
        }

        public async Task<User> UpdateUserAsync(int userId, UpdateUserRequestModel updateUser)
        {
            string json = JsonConvert.SerializeObject(updateUser);

            return await PutAsync<User>($"/v{APIVERSION}/users/{userId}", json);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await DeleteAsync($"/v{APIVERSION}/users/{userId}");
        }

        public async Task MarkUserEntriesAsReadAsync(int userId)
        {
            await PutAsync($"/v{APIVERSION}/users/{userId}/mark-all-as-read");
        }

        public async Task<User> MeAsync()
        {
            return await GetAsync<User>($"/v{APIVERSION}/me");
        }

        public async Task<CountersResponseModel> FetchCounters()
        {
            return await GetAsync<CountersResponseModel>($"/v{APIVERSION}/feeds/counters");
        }


        public async Task<string> HealthCheckAsync()
        {
            return await GetAsync("/healthcheck");
        }

        public async Task<string> VersionAsync()
        {
            return await GetAsync("/version");
        }


        public async Task<string> Export()
        {
            return await GetAsync($"/v{APIVERSION}/export");
        }

        public async Task<ClientResponseModel> Import(byte[] file)
        {
            return await PostAsync<ClientResponseModel>($"/v{APIVERSION}/import", file);
        }


        private string ToBase64String(string text)
        {
            return Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(text));
        }
    }
}
