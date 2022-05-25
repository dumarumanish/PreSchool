using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Dtos
{
    public class NotificationGroupDto
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public string Description { get; set; }
        public List<NotificationActivityTypeDto> SubscribedActivities { get; set; }
    }
}
