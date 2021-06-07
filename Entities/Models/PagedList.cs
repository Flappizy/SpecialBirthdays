using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.RequestFeatures;

namespace Entities.Models
{
    //This class is used to handle pagination
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData;

        public PagedList(IEnumerable<T> items, int count, int pageSize, int pageNumber)
        {
            MetaData = new MetaData
            {
                PageSize = pageSize,
                TotalCount = count,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)

            };
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageSize, int pageNumber)
        {
            int count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
    }
}
