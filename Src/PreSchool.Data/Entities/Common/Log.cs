using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities
{
    public class Log
    {
        public int Id { get; set; }


        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Parameters { get; set; }

        public int ActivityTypeId { get; set; }

        public int? AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual NotificationActivityType ActivityType { get; set; }
    }
}
