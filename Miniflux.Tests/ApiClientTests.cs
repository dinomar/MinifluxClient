using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Miniflux.Clients;
using Miniflux.Models;
using Xunit;

namespace Miniflux.Tests
{
    public class ApiClientTests
    {
        private static readonly string _minifluxUrl = "http://192.168.1.100";

        [Fact]
        public async Task CanGetPageAsStringAsync()
        {
            // Arrange
            string expectedContent = "Sign In - Miniflux";
            BaseRESTClient client = new BaseRESTClient(_minifluxUrl);

            // Act
            string result = await client.GetAsync("");

            // Assert
            Assert.Contains(expectedContent, result);
        }

        [Fact]
        public void ThrowsClientExceptionOnInvalidRequest()
        {
            // Arrange
            BaseRESTClient client = new BaseRESTClient(_minifluxUrl);

            // Act Assert
            Assert.ThrowsAsync<ClientException>(async () =>
            {
                await client.GetAsync("/v1/me");
            });
        }
    }
}
