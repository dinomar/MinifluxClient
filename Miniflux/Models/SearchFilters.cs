using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Models
{
    public enum StatusFilter
    {
        None,
        Read,
        Unread,
        Removed
    }

    public enum OrderFilter
    {
        Id,
        Status,
        PublishedAt,
        CategoryTitle,
        CategoryId
    }

    public enum DirectionFilter
    {
        None,
        Asc,
        Desc
    }

    public class SearchFilters
    {
        public StatusFilter Status { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public DirectionFilter Direction { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public Int64 BeforeEntryId { get; set; }
        public Int64 AfterEntryId { get; set; }
        public bool Starred { get; set; }
        public string Search { get; set; }
        public int CategoryId { get; set; }


        public string GetQueryString()
        {
            StringBuilder sb = new StringBuilder();
            bool firstItem = true;

            sb.Append("?");

            // Status
            if (Status != StatusFilter.None)
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"{nameof(Status)}={Enum.GetName(typeof(StatusFilter), Status)}");
            }

            // Offset
            if (Offset > 0)
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"{nameof(Offset)}={Offset}");
            }

            // Limit
            if (Limit > 0)
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"{nameof(Limit)}={Limit}");
            }

            // Direction
            if (Direction != DirectionFilter.None)
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"{nameof(Direction)}={Enum.GetName(typeof(DirectionFilter), Direction)}");
            }

            // Before
            if (!string.IsNullOrEmpty(Before))
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"{nameof(Before)}={Before}");
            }

            // After
            if (!string.IsNullOrEmpty(After))
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"{nameof(After)}={After}");
            }

            // BeforeEntryId
            if (BeforeEntryId > 0)
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"before_entry_id={BeforeEntryId}");
            }

            // AfterEntryId
            if (AfterEntryId > 0)
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"after_entry_id={AfterEntryId}");
            }

            // Starred
            if (Starred)
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"{nameof(Starred)}={Starred}");
            }

            // Search
            if (!string.IsNullOrEmpty(Search))
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"{nameof(Search)}={Search}");
            }

            // CategoryId
            if (CategoryId > 0)
            {
                if (!firstItem) { sb.Append("&"); } else { firstItem = false; }
                sb.Append($"category_id={CategoryId}");
            }

            return sb.ToString().ToLower();
        }

        public override string ToString()
        {
            return GetQueryString();
        }
    }
}
