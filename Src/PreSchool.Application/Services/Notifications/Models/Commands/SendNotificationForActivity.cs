using PreSchool.Application.Services.Notifications.Models.Commands;
using PreSchool.Data.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications
{
    public class SendNotificationForActivity
    {
        public NotificationActivityTypeEnum NotificationActivityTypeId { get; set; }
        public NotificationMessage PushNotification { get; set; }
        public NotificationMessage EmailNotification { get; set; }
        public NotificationMessage SMSNotification { get; set; }
        public int? SenderId { get; set; }

        /// <summary>
        ///  Id of the entity that the activity is related to.
        ///  eg: If the activity type is book published then the source id refers to the ID of a book.
        /// </summary>
        public int? SourceEntityId { get; set; }

        public SendNotificationForActivity()
        {

        }
    }

}
