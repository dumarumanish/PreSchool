using PreSchool.Data.Entities.AppUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Notifications
{
    public class NotificationGroupSubscriber : CommonProperties
    {
        public int NotificationGroupId { get; set; }
        public int AppUserId { get; set; }
        public bool Email { get; set; }
        public bool PushNotification { get; set; }
        public bool SMS { get; set; }
        public virtual NotificationGroup NotificationGroup { get; set; }

        public virtual AppUser AppUser { get; set; }

    }
}
