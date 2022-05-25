using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Notifications
{
    /// <summary>
    /// Type of notification
    /// </summary>
    public enum NotificationTypeEnum
    {
        [Display(Name = "Email", Description = "Notification send via email")]
        Email = 1,

        [Display(Name = "Mobile", Description = "Notification send to mobile")]
        Mobile,

        [Display(Name = "Push Notification", Description = "Notification send as a push notification")]
        PushNotification,

        [Display(Name = "Chat Message", Description = "Chat message")]
        ChatMessage

    }
}
