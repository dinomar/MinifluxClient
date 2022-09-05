using Miniflux.Models;
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
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Headers.Add("Authorization", "Basic " + ToBase64String($"{username}:{password}"));
            }
        }

        public MinifluxClient(string baseUrl, string apiKey, int timeout = 30, Dictionary<string, string> headers = null)
            : base(baseUrl, timeout, headers)
        {
            if (!string.IsNullOrEmpty(apiKey))
            {
                Headers.Add("X-Auth-Token", apiKey);
            }
        }


        public async Task<IEnumerable<Subscription>> Discover(string url)
        {
            DiscoverRequest request = new DiscoverRequest() { Url = url };
            string json = JsonConvert.SerializeObject(request);

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

        public async Task<CreateFeedResponse> CreateFeedAsync(CreateFeedRequest createFeed)
        {
            string json = JsonConvert.SerializeObject(createFeed);

            return await PostAsync<CreateFeedResponse>($"/v{APIVERSION}/feeds", json);
        }

        public async Task<Feed> UpdateFeedAsync(int feedId, UpdateFeedRequest updateFeed)
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

        public async Task<EntriesResponse> GetFeedEntriesAsync(int feedId)
        {
            return await GetAsync<EntriesResponse>($"/v{APIVERSION}/feeds/{feedId}/entries");
        }

        public async Task MarkFeedEntriesAsRead(int feedId)
        {
            await PutAsync($"/v{APIVERSION}/feeds/{feedId}/mark-all-as-read");
        }

        public async Task<Entry> GetEntryAsync(int entryId)
        {
            return await GetAsync<Entry>($"/v{APIVERSION}/entries/{entryId}");
        }

        public async Task<EntriesResponse> GetEntriesAsync()
        {
            return await GetAsync<EntriesResponse>($"/v{APIVERSION}/entries");
        }

        public async Task UpdateEntriesAsync(UpdateEntriesRequest updateEntries)
        {
            string json = JsonConvert.SerializeObject(updateEntries);

            await PutAsync($"/v{APIVERSION}/entries", json);
        }

        public async Task ToggleBookmarkAsync(int entryId)
        {
            await PutAsync($"/v{APIVERSION}/entries/{entryId}/bookmark");
        }


        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await GetAsync<IEnumerable<Category>>($"/v{APIVERSION}/categories");
        }

        public async Task<Category> CreateCategoryAsync(CreateUpdateCategoryRequest createCategory)
        {
            string json = JsonConvert.SerializeObject(createCategory);

            return await PostAsync<Category>($"/v{APIVERSION}/categories", json);
        }

        public async Task<Category> UpdateCategoryAsync(int categoryId, CreateUpdateCategoryRequest updateCategory)
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

        public async Task<User> CreateUserAsync(CreateUserRequest createUser)
        {
            string json = JsonConvert.SerializeObject(createUser);

            return await PostAsync<User>($"/v{APIVERSION}/users", json);
        }

        public async Task<User> UpdateUserAsync(int userId, UpdateUserRequest updateUser)
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

        public async Task<ClientResponse> Import(byte[] file)
        {
            return await PostAsync<ClientResponse>($"/v{APIVERSION}/import", file);
        }


        private string ToBase64String(string text)
        {
            return Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(text));
        }
    }
}
