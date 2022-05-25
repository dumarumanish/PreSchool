using System.Collections.Generic;
using System.Threading.Tasks;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using PreSchool.Application.Services.Notifications.Models.Commands;
using PreSchool.Application.Services.Notifications.Models.Dtos;
using PreSchool.Application.Services.Notifications.Models.Queries;

namespace PreSchool.Application.Services.Notifications
{
    public interface INotificationService
    {
        Task<bool> DeleteNotification(int notificationId);
        Task<NotificationDto> GetNotificationById(int notificationId);
        Task<PagedResult<NotificationListDto>> GetNotifications(int appUserId, NotificationFilter filter);
        Task<bool> MarkNotificationAsAcknowledged(int notificationId);
        Task<bool> MarkNotificationAsRead(int notificationId);
        Task<List<AppUserListDto>> OnlineUsers();
        Task<bool> SendPushNotification(InsertNotificationCommand notification);

        Task<bool> UpdateNotificationActivityType(UpdateNotificationActivityType activityType);
        Task<List<NotificationActivityTypeDto>> GetNotificationAcitivityTypes();
        Task<bool> Notification(SendNotificationCommand notification);



        #region Notificaiton Group

        Task<int> InsertUpdateNotificationGroup(InsertUpdateNotificationGroup notificationGroup);

        Task<NotificationGroupDto> GetNotificationGroup(int id);

        Task<PagedResult<NotificationGroupDto>> GetNotificationGroups(NotificationGroupFilter filter);
        Task<bool> DeleteNotificationGroup(int notificationGroupId);


        Task<bool> AddNotificationGroupSubscribedActivity(InsertNotificationGroupSubscribedActivity subscribedActivity);

        Task<bool> RemoveNotificationGroupSubscribedActivity(InsertNotificationGroupSubscribedActivity subscribedActivity);


        Task<bool> AddNotificationGroupSubscriber(List<AddNotificationGroupSubscriber> subscribers);

        Task<bool> RemoveNotificationGroupSubscriber(RemoveNotificationGroupSubscriber subscriber);


        Task<bool> SendNotificationForActivity( SendNotificationForActivity notification);

        Task<List<NotificationActivityTypeDto>> NotificationGroupSubscribedActivity(int notificationGroupId);

        Task<List<NotificationGroupSubscriberDto>> NotificationGroupSubscribers(int notificationGroupId);
        Task<List<AppUserSubscribedActivityDto>> AppUserSubscribedActivities(int appuserId);
        #endregion
    }
}