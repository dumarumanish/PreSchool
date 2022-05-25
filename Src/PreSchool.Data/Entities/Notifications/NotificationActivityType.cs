using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Notifications
{
    /// <summary>
    /// Type of the activity of the user for the notification
    /// eg: Post a comment, send friend requiest
    /// </summary>
    public class NotificationActivityType
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        /// <summary>
        /// Name of the activity
        /// </summary>
        public string Name { get; set; }

        public string DisplayName { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Redirect url of the client page when user clicked on the notification
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Redirect url of the admin page when user clicked on the notification
        /// </summary>
        public string AdminRedirectUrl { get; set; }

    }
}
