using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Notifications.Events
{
    public class NewNotificationReceivedEvent
    {
        public NewNotificationReceivedEvent(Notification notification, NotificationActivityType notificationActivityType)
        {
            Notification = notification;
            NotificationActivityType = notificationActivityType;
        }

        public Notification Notification { get; }
        public NotificationActivityType NotificationActivityType { get; }
    }
}
