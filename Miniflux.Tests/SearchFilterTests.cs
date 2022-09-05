using Miniflux.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Miniflux.Tests
{
    public class SearchFilterTests
    {
        [Fact]
        public void CanGenerateSearchFilterQueryString()
        {
            // Arrange
            SearchFilters filter = new SearchFilters();
            filter.Status = StatusFilter.Read;
            filter.Offset = 1;
            filter.Limit = 5;
            filter.Direction = DirectionFilter.Asc;
            filter.BeforeEntryId = 10;
            filter.AfterEntryId = 5;
            filter.Starred = true;
            filter.Search = "helloworld";
            filter.CategoryId = 10;

            // Act
            string query = filter.GetQueryString();

            // Assert
            Assert.Contains("status=read", query);
            Assert.Contains("offset=1", query);
            Assert.Contains("limit=5", query);
            Assert.Contains("direction=asc", query);
            Assert.Contains("before_entry_id=10", query);
            Assert.Contains("after_entry_id=5", query);
            Assert.Contains("starred=true", query);
            Assert.Contains("search=helloworld", query);
            Assert.Contains("category_id=10", query);
        }
    }
}
