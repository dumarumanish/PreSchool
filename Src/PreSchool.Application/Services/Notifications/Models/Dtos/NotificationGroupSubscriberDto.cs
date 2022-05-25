using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Dtos
{
    public class NotificationGroupSubscriberDto
    {
        public int NotificationGroupId { get; set; }
        public int AppUserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public bool Email { get; set; }
        public bool PushNotification { get; set; }
        public bool SMS { get; set; }
    }
}
