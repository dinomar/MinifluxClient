using Miniflux.Clients;
using Miniflux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Miniflux.Tests
{
    public class MinifluxClientTests
    {
        private static readonly string _minifluxUrl = "http://192.168.1.100";
        private static readonly string _minifluxUsername = "admin";
        private static readonly string _minifluxPassword = "admin";
        private static readonly string _minifluxApiKey = "";


        [Fact]
        public async Task CanAuthenticateWithUsernameAndPassword()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxUsername, _minifluxPassword);

            // Act
            User user = await client.MeAsync();

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public async Task CanAuthenticateWithApiKey()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            User user = await client.MeAsync();

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public async Task CanDiscoverSubscriptions()
        {
            // Arrange
            string url = "https://www.debian.org/";
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            IEnumerable<Subscription> result = await client.Discover(url);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetFeeds()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            IEnumerable<Feed> result = await client.GetFeedsAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetFeed()
        {
            // Arrange
            int feedId = 5;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            Feed result = await client.GetFeedAsync(feedId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetFeedIcon()
        {
            // Arrange
            int feedId = 5;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            Icon result = await client.GetFeedIconAsync(feedId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanCreateFeed()
        {
            // Arrange
            CreateFeedRequest newFeed = new CreateFeedRequest
            {
                FeedUrl = "https://www.debian.org/News/news",
                CategoryId = 1
            };

            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            CreateFeedResponse result = await client.CreateFeedAsync(newFeed);
            await client.DeleteFeedAsync(result.FeedId);

            // Assert
            Assert.NotNull(result); // 70
        }

        [Fact]
        public async Task CanUpdateFeed()
        {
            // Arrange
            CreateFeedRequest newFeed = new CreateFeedRequest
            {
                FeedUrl = "https://www.debian.org/News/news",
                CategoryId = 1
            };

            UpdateFeedRequest updatefeed = new UpdateFeedRequest
            {
                Title = "New title",
                CategoryId = 1
            };

            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            CreateFeedResponse createResult = await client.CreateFeedAsync(newFeed);

            // Act
            Feed updateResult = await client.UpdateFeedAsync(createResult.FeedId, updatefeed);
            
            await client.DeleteFeedAsync(updateResult.Id);

            // Assert
            Assert.NotNull(updateResult);
        }

        [Fact]
        public async Task CanDeleteFeed()
        {
            // Arrange
            CreateFeedRequest newFeed = new CreateFeedRequest
            {
                FeedUrl = "https://www.debian.org/News/news",
                CategoryId = 1
            };

            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);
            CreateFeedResponse result = await client.CreateFeedAsync(newFeed);

            // Act Assert
            await client.DeleteFeedAsync(result.FeedId);
        }

        [Fact]
        public async Task CanRefreshFeed()
        {
            // Arrange
            int feedId = 1;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act Assert
            await client.RefreshFeedAsync(feedId);
        }

        [Fact]
        public async Task CanRefreshAllFeeds()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act Assert
            await client.RefreshAllFeedsAsync();
        }

        [Fact]
        public async Task CanGetFeedEntry()
        {
            // Arrange
            int feedId = 63;
            int entryId = 4018;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            Entry result = await client.GetFeedEntryAsync(feedId, entryId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetFeedEntries()
        {
            // Arrange
            int feedId = 63;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            EntriesResponse results = await client.GetFeedEntriesAsync(feedId);

            // Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task CanMarkAllFeedEntriesAsRead()
        {
            // Arrange
            int feedId = 63;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act Assert
            await client.MarkFeedEntriesAsRead(feedId);
        }

        [Fact]
        public async Task CanGetEntry()
        {
            // Arrange
            int entryId = 4018;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            Entry result = await client.GetEntryAsync(entryId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetEntries()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            EntriesResponse result = await client.GetEntriesAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanUpdateEntries()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);
            UpdateEntriesRequest updateEntries = new UpdateEntriesRequest
            {
                entryIds = new int[] { 4055, 4054 },
                Status = "read"
            };

            // Act Assert
            await client.UpdateEntriesAsync(updateEntries);
        }

        [Fact]
        public async Task CanToggleBookmarked()
        {
            // Arrange
            int entryId = 4016;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act Assert
            await client.ToggleBookmarkAsync(entryId);
        }

        [Fact]
        public async Task CanGetCategories()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            IEnumerable<Category> results = await client.GetCategoriesAsync();

            // Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task CanCreateCategory()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);
            CreateUpdateCategoryRequest createCategory = new CreateUpdateCategoryRequest
            {
                Title = "New Category"
            };

            // Act
            Category result = await client.CreateCategoryAsync(createCategory);

            await client.DeleteCategoryAsync(result.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanUpdateCategory()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            CreateUpdateCategoryRequest createCategory = new CreateUpdateCategoryRequest
            {
                Title = "New Category"
            };

            CreateUpdateCategoryRequest updateCategory = new CreateUpdateCategoryRequest
            {
                Title = "New Title"
            };

            Category createResult = await client.CreateCategoryAsync(createCategory);

            // Act
            Category updateResult = await client.UpdateCategoryAsync(createResult.Id, updateCategory);

            await client.DeleteCategoryAsync(updateResult.Id);

            // Assert
            Assert.NotNull(updateResult);
        }

        [Fact]
        public async Task CanDeleteCategory()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            CreateUpdateCategoryRequest createCategory = new CreateUpdateCategoryRequest
            {
                Title = "New Category"
            };

            Category createResult = await client.CreateCategoryAsync(createCategory);

            // Act Assert
            await client.DeleteCategoryAsync(createResult.Id);
        }

        [Fact]
        public async Task CanMarkCategoryEntriesAsRead()
        {
            // Arrange
            int categoryId = 2;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act Assert
            await client.MarkCategoryEntriesAsReadAsync(categoryId);
        }

        [Fact]
        public async Task CanGetUsers()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            IEnumerable<User> results = await client.GetUsersAsync();

            // Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task CanGetUserById()
        {
            // Arrange
            int userId = 1;
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            User result = await client.GetUserAsync(userId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetUserByUsername()
        {
            // Arrange
            string username = "admin";
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            User result = await client.GetUserAsync(username);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanCreateUser()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);
            CreateUserRequest createUser = new CreateUserRequest
            {
                Username = "user1",
                Password = "User123",
                IsAdmin = false
            };

            // Act
            User result = await client.CreateUserAsync(createUser);

            await client.DeleteUserAsync(result.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanUpdateUser()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            CreateUserRequest createUser = new CreateUserRequest
            {
                Username = "user1",
                Password = "User123",
                IsAdmin = false
            };

            UpdateUserRequest updateUser = new UpdateUserRequest
            {
                Username = "user2"
            };

            User createResult = await client.CreateUserAsync(createUser);

            // Act
            User updateResult = await client.UpdateUserAsync(createResult.Id, updateUser);

            await client.DeleteUserAsync(createResult.Id);

            // Assert
            Assert.NotNull(updateResult);
        }

        [Fact]
        public async Task CanDeleteUser()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            CreateUserRequest createUser = new CreateUserRequest
            {
                Username = "user1",
                Password = "User123",
                IsAdmin = false
            };

            User result = await client.CreateUserAsync(createUser);

            // Act Assert
            await client.DeleteUserAsync(result.Id);
        }

        [Fact]
        public async Task CanMarkUserEntriesAsRead()
        {
            // Arrange
            MinifluxClient adminClient = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            CreateUserRequest createUser = new CreateUserRequest
            {
                Username = "user1",
                Password = "User123",
                IsAdmin = false
            };

            User result = await adminClient.CreateUserAsync(createUser);

            MinifluxClient userClient = new MinifluxClient(_minifluxUrl, createUser.Username, createUser.Password);

            // Act Assert
            await userClient.MarkUserEntriesAsReadAsync(result.Id);

            await adminClient.DeleteUserAsync(result.Id);
        }


        [Fact]
        public async Task CanGetHealthCheck()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, null);

            // Act
            string result = await client.HealthCheckAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetVersion()
        {
            // Arrange
            MinifluxClient client = new MinifluxClient(_minifluxUrl, null);

            // Act
            string result = await client.VersionAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, char.IsDigit);
        }


        [Fact]
        public async Task CanExport()
        {
            // Arrange
            string expected = "xml";
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            string result = await client.Export();

            // Assert
            Assert.NotNull(result);
            Assert.Contains(expected, result);
        }

        [Fact]
        public async Task CanImport()
        {
            // Arrange
            string expected = "Feeds imported successfully";
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<opml version=\"2.0\">\r\n    <head>\r\n        <title>Miniflux</title>\r\n        <dateCreated>Mon, 05 Sep 2022 13:56:40 UTC</dateCreated>\r\n    </head>\r\n    <body>\r\n        <outline text=\"Blog - Tech\">\r\n            <outline title=\"Debian News\" text=\"Debian News\" xmlUrl=\"https://www.debian.org/News/news\" htmlUrl=\"https://www.debian.org/News/\"></outline>\r\n        </outline>\r\n    </body>\r\n</opml>";
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xml);
            MinifluxClient client = new MinifluxClient(_minifluxUrl, _minifluxApiKey);

            // Act
            ClientResponse result = await client.Import(xmlBytes);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(expected, result.Message);
        }
    }
}
