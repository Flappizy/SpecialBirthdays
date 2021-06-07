using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class PersonsBirhtdaysParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 6;
        public string UserId { get; set; }
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; } = "name";

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
