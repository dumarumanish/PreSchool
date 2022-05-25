using PreSchool.Application.Services.Notifications.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Notifications
{
    public interface IHubNotificationHelper
    {
        void SendNotificationToAll(string message);
        IEnumerable<int> GetOnlineUsers();
        Task<Task> SendNotificationParallel(int appUserId, string message);
        Task<Task> SendRadiusCustomerSyncNotificationParallel(int appUserId, RadiusCustomerSyncNotificationDto message);
    }
}
