using System.Collections.Generic;

namespace Loaf.Core.Data
{
    public class PagedResult<TResult> where TResult : class
    {
        public PagedResult(int total, List<TResult> items)
        {
            Total = total;
            Items = items;
        }

        public int Total { get; set; }
        public List<TResult> Items { get; set; }
    }

    public class PageQueryParameter : IPagination
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string OrderBy { get; set; }
    }
}