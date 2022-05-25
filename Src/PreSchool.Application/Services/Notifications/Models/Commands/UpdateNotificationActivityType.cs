using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Commands
{
    public class UpdateNotificationActivityType
    {

        public int Id { get; set; }

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
