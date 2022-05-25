using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Queries
{
    public class NotificationGroupFilter : PaginationFilter
    {
        public int? IspId { get; set; }
        public string Search { get; set; }
    }
}
