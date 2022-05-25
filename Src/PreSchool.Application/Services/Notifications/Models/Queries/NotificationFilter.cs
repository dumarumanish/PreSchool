using System;
using System.Collections.Generic;
using System.Text;
using PreSchool.Application.Models;

namespace PreSchool.Application.Services.Notifications.Models.Queries
{
    public class NotificationFilter: PaginationFilter
    {
        public int? ActivityTypeId { get; set; }
        public string Search { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
