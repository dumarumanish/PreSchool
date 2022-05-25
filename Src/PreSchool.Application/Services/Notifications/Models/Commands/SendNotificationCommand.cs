using System.Collections.Generic;

namespace PreSchool.Application.Services.Notifications
{
    public class SendNotificationCommand
    {
        public NotificationMessage PushNotification { get; set; }
        public NotificationMessage EmailNotification { get; set; }
        public NotificationMessage SMSNotification { get; set; }

        public List<NotificationRecipient> Recipients { get; set; }
    }
    public class NotificationMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }


    public class NotificationRecipient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? AppUserId { get; set; }
        public string Username { get; set; }
    }
}
