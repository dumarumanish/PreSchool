using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Dtos
{
    public class AppUserSubscribedActivityDto
    {
        public int NotificationGroupId { get; set; }
        public string NotificationGroupName { get; set; }
        public int NotificationActivityTypeId { get; set; }
        public string ActivityTypeName { get; set; }

        public bool Email { get; set; }
        public bool PushNotification { get; set; }
        public bool SMS { get; set; }
    }
}
