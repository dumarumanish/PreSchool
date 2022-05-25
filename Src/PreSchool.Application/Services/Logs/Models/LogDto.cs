using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Logs.Models
{
    public class LogDto
    {
        public int Id { get; set; }

        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Parameters { get; set; }

        public int ActivityTypeId { get; set; }
        public string ActivityType { get; set; }
        public int? AppUserId { get; set; }
        public string AppUser { get; set; }
    }
}
