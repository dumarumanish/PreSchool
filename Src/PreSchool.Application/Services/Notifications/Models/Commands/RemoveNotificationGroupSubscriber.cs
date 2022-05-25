using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Commands
{
    public class RemoveNotificationGroupSubscriber
    {
        public int NotificationGroupId { get; set; }
        public int AppUserId { get; set; }
    }
}
