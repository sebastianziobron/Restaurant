using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Models
{
    public class PageResault<T>
    {
        public List<T> Items {get; set;}

        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalItemCount { get; set; }

        public PageResault(List<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemCount = totalCount;
            ItemFrom = pageSize * (pageNumber - 1) + 1;
            ItemTo = ItemFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
