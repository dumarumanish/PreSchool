using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Commands
{
    public class InsertNotificationCommand
    {
        public int ActivityTypeId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public int? SenderId { get; set; }
        public int RecipientId{ get; set; }

        /// <summary>
        ///  Id of the entity that the activity is related to.
        ///  eg: If the activity type is book published then the source id refers to the ID of a book.
        /// </summary>
        public int? SourceEntityId { get; set; }

    }
}
