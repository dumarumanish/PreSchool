using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Commands
{
    public class AddNotificationGroupSubscriber
    {
        public int NotificationGroupId { get; set; }
        public int AppUserId { get; set; }
        public bool Email { get; set; }
        public bool PushNotification { get; set; }
        public bool SMS { get; set; }
    }
}
