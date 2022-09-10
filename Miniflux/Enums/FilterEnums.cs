using System;
using System.Collections.Generic;
using System.Text;

namespace Miniflux.Enums
{
    public enum StatusFilter
    {
        Read,
        Unread,
        Removed
    }

    public enum OrderFilter
    {
        Id,
        Status,
        Published_At,
        Category_Title,
        Category_Id
    }

    public enum DirectionFilter
    {
        Asc,
        Desc
    }
}
