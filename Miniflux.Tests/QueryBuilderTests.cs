using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Miniflux.Helpers;
using Miniflux.Enums;

namespace Miniflux.Tests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void CanAddItem()
        {
            // Arrange
            string expected = "?offset=5";
            QueryBuilder query = new QueryBuilder();
            int? offset = 5;

            // Act
            query.AddItem(nameof(offset), offset);
            string queryString = query.ToString();

            // Assert
            Assert.NotNull(queryString);
            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanAddEnum()
        {
            // Arrange
            string expected = "?direction=desc";
            QueryBuilder query = new QueryBuilder();
            DirectionFilter direction = DirectionFilter.Desc;

            // Act
            query.AddEnum(nameof(direction), direction);
            string queryString = query.ToString();

            // Assert
            Assert.NotNull(queryString);
            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanAddMultipleSelectionEnum()
        {
            // Arrange
            string expected = "?status=['read','unread']";
            QueryBuilder query = new QueryBuilder();
            StatusFilter status = StatusFilter.Read | StatusFilter.Unread;

            // Act
            query.AddEnum(nameof(status), status);
            string queryString = query.ToString();

            // Assert
            Assert.NotNull(queryString);
            Assert.Equal(expected, queryString);
        }
    }
}
