using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Dtos
{
    public class NotificationListDto
    {
        public int Id { get; set; }
        public int ActivityTypeId { get; set; }
        public NotificationActivityTypeDto ActivityType { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Sender of the notification
        /// May be null if the notification is sent from system
        /// </summary>
        public int? SenderId { get; set; }

        public int? RecipientId { get; set; }

        /// <summary>
        ///  Id of the entity that the activity is related to.
        ///  eg: If the activity type is book published then the source id refers to the ID of a book.
        /// </summary>
        public int? SourceEntityId { get; set; }

        public DateTime SentDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsSent { get; set; }
    }
}
