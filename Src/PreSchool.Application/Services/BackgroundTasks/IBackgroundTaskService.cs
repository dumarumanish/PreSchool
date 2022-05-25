using System;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.BackgroundTasks
{
    public interface IBackgroundTaskService
    {
        void DelayedTask(Task task, int delayInMinutes);
        void FireAndForgotTask(Task task);
        void SendUnSentNotifications();
    }
}