using System.Collections.Generic;

namespace PreSchool.Data.Entities.Notifications
{
    public class NotificationGroup : CommonProperties
    {
      
        public string Name { get; set; }
        public string Description { get; set; }



        public virtual ICollection<NotificationGroupSubscriber> NotificationGroupSubscribers { get; set; }


        public virtual ICollection<NotificationGroupSubscribedActivity> NotificationGroupSubscribedActivities { get; set; }


        public NotificationGroup()
        {
            NotificationGroupSubscribers = new HashSet<NotificationGroupSubscriber>();
            NotificationGroupSubscribedActivities = new HashSet<NotificationGroupSubscribedActivity>();

        }

    }
}
