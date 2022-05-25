using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Notifications
{
    public class NotificationGroupSubscribedActivity : CommonProperties
    {

        public int NotificationGroupId { get; set; }


        public int NotificationActivityTypeId { get; set; }
        public virtual NotificationActivityType NotificationActivityType { get; set; }

        public virtual NotificationGroup NotificationGroup { get; set; }

    }
}
