using PreSchool.Application.Events;
using PreSchool.Data.Entities.Notifications;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application
{
    public static class Logger
    {
        public static void Activity(string message, NotificationActivityTypeEnum activityType, EventSender sender, object parameter = null)
        {
            if (sender == null)
                sender = new EventSender();
            Log
                .ForContext("IsActivity", true)
                .ForContext("AppUserId", sender.AppUserId)
                .ForContext("ActivityTypeId", (int)activityType)
                .ForContext("Parameters", JsonConvert.SerializeObject(parameter))
                .Information(message);
        }

        public static void Error(Exception ex, EventSender sender = null, object parameter = null)
        {
            if (sender == null)
                sender = new EventSender();
            Log
                .ForContext("IsActivity", true)
                .ForContext("AppUserId", sender.AppUserId)
                .ForContext("ActivityTypeId", (int)NotificationActivityTypeEnum.None)
                .ForContext("Parameters", JsonConvert.SerializeObject(parameter))
                .Error(ex, ex.Message);
        }

        public static void Fatal(Exception ex, EventSender sender = null, object parameter = null)
        {
            if (sender == null)
                sender = new EventSender();
            Log
                .ForContext("IsActivity", true)
                .ForContext("AppUserId", sender.AppUserId)
                .ForContext("ActivityTypeId", (int)NotificationActivityTypeEnum.None)
                .ForContext("Parameters", JsonConvert.SerializeObject(parameter))
                .Fatal(ex, ex.Message);
        }
    }
}
