using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Commands
{
    public class InsertUpdateNotificationGroup
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
