using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Commands
{
    public class InsertNotificationGroupSubscribedActivity
    {
        public int NotificationGroupId { get; set; }
        public List<int> NotificationActivityTypeIds { get; set; }
    }
}
