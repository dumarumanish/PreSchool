using PreSchool.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int? PageSize { get; set; }
        
        public List<SortModel> SortBy { get; set; }
        public PaginationFilter()
        {
            // Default page number
            PageNumber = 1;
            SortBy = new List<SortModel>();
        }
    }
}
