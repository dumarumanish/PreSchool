using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Logs.Models
{
    public class LogFilter : PaginationFilter
    {
        public int? AppUserId { get; set; }
        public int? ActivityTypeId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

    }
}
