using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreSchool.Application;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Models.AppUsers.Queries;
using PreSchool.Application.Services.AppUsers;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using PreSchool.Application.Services.AppUsers.Models.Queries;
using PreSchool.Application.Services.Notifications;
using PreSchool.Application.Services.Notifications.Models.Commands;
using PreSchool.Application.Services.Notifications.Models.Dtos;
using PreSchool.Application.Services.Notifications.Models.Queries;
using System.Linq;
using PreSchool.Data.Enumerations;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        #region Notification Activity Type

        [HttpGet("ActivityTypes")]
        public async Task<List<NotificationActivityTypeDto>> GetNotificationAcitivityTypes()
        {
            return await _notificationService.GetNotificationAcitivityTypes();

        }

        [HttpPut("ActivityTypes/{id}")]
        public async Task<bool> UpdateNotificationActivityType(int id, UpdateNotificationActivityType activityType)
        {

            if (id != activityType.Id)
                throw new BadRequestException("Id doesnot match");
            return await _notificationService.UpdateNotificationActivityType(activityType);
        }
        #endregion


        [HttpGet("AppUser/{appUserId}")]
        public async Task<PagedResult<NotificationListDto>> GetNotifications(int appUserId, [FromQuery] NotificationFilter filter)
        {
            return await _notificationService.GetNotifications(appUserId, filter);
        }

        [HttpGet("{notificationId}")]
        public async Task<NotificationDto> GetNotificationById(int notificationId)
        {
            return await _notificationService.GetNotificationById(notificationId);

        }

        [HttpPost("{notificationId}/MarkRead")]
        public async Task<bool> MarkNotificationAsRead(int notificationId)
        {

            return await _notificationService.MarkNotificationAsRead(notificationId);

        }

        [HttpPost("{notificationId}/MarkAcknowledge")]
        public async Task<bool> MarkNotificationAsAcknoledged(int notificationId)
        {
            return await _notificationService.MarkNotificationAsAcknowledged(notificationId);


        }

        [HttpDelete("{notificationId}")]
        public async Task<bool> DeleteNotification(int notificationId)
        {
            return await _notificationService.DeleteNotification(notificationId);


        }

        /// <summary>
        /// Send notification for testing
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        [AuthorizeUser(Permissions.SendNotifications)]
        [HttpPost("Test")]
        public async Task<bool> TestNotification(InsertNotificationCommand notification)
        {
            return await _notificationService.SendPushNotification(notification);

        }

        [HttpPost]
        public async Task<bool> Notification(SendNotificationCommand notification)
        {
            return await _notificationService.Notification(notification);

        }

        [HttpGet]
        [Route("~/api/OnlineUsers")] //routes to `/api/token`
        public async Task<List<AppUserListDto>> OnlineUsers()
        {
            return await _notificationService.OnlineUsers();
        }



        #region Notificaiton Group

        [HttpPost("Groups")]
        public async Task<int> InsertUpdateNotificationGroup(InsertUpdateNotificationGroup notificationGroup)
        {
            notificationGroup.Id = 0;
            return await _notificationService.InsertUpdateNotificationGroup(notificationGroup);
        }

        [HttpPut("Groups/{id}")]
        public async Task<int> InsertUpdateNotificationGroup(int id, InsertUpdateNotificationGroup notificationGroup)
        {
            if (id != notificationGroup.Id)
                throw new BadRequestException("Id doesnot match");

            if (id == 0)
                throw new BadRequestException("Id cannot be 0");

            return await _notificationService.InsertUpdateNotificationGroup(notificationGroup);
        }


        [HttpGet("Groups/{id}")]
        public async Task<NotificationGroupDto> GetNotificationGroup(int id)
        {

            return await _notificationService.GetNotificationGroup(id);
        }

        [HttpGet("Groups")]
        public async Task<PagedResult<NotificationGroupDto>> GetNotificationGroups([FromQuery] NotificationGroupFilter filter)
        {
            return await _notificationService.GetNotificationGroups(filter);
        }


        [HttpDelete("Groups/{id}")]
        public async Task<bool> DeleteNotificationGroup(int id)
        {

            return await _notificationService.DeleteNotificationGroup(id);

        }

        /// <summary>
        /// Add update subscribed activity of the notification group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="subscribedActivity"></param>
        /// <returns></returns>
        [HttpPost("Groups/{groupId}/SubscribedActivity")]
        public async Task<bool> AddNotificationGroupSubscribedActivity(int groupId, InsertNotificationGroupSubscribedActivity subscribedActivity)
        {
            if (groupId != subscribedActivity.NotificationGroupId)
                throw new BadRequestException("Id doesnot match");

            return await _notificationService.AddNotificationGroupSubscribedActivity(subscribedActivity);
        }

        /// <summary>
        /// Remove the subscribed activity from the notification group
        /// </summary>
        /// <param name="subscribedActivity"></param>
        /// <returns></returns>
        [HttpDelete("Groups/{groupId}/SubscribedActivity")]
        public async Task<bool> RemoveNotificationGroupSubscribedActivity(int groupId, InsertNotificationGroupSubscribedActivity subscribedActivity)
        {
            if (groupId != subscribedActivity.NotificationGroupId)
                throw new BadRequestException("Id doesnot match");

            return await _notificationService.RemoveNotificationGroupSubscribedActivity(subscribedActivity);
        }


        /// <summary>
        /// Add subscriber (Appusers) to notification group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="subscribers"></param>
        /// <returns></returns>
        [HttpPost("Groups/{groupId}/Subscriber")]
        public async Task<bool> AddNotificationGroupSubscriber(int groupId, List<AddNotificationGroupSubscriber> subscribers)
        {
            if (groupId != subscribers?.First().NotificationGroupId)
                throw new BadRequestException("Id doesnot match");

            return await _notificationService.AddNotificationGroupSubscriber(subscribers);
        }

        /// <summary>
        /// Remove subscriber from notification group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        [HttpDelete("Groups/{groupId}/Subscriber")]
        public async Task<bool> RemoveNotificationGroupSubscriber(int groupId, RemoveNotificationGroupSubscriber subscriber)
        {
            if (groupId != subscriber.NotificationGroupId)
                throw new BadRequestException("Id doesnot match");

            return await _notificationService.RemoveNotificationGroupSubscriber(subscriber);
        }

        /// <summary>
        /// Send notification to all subscribed users for particular activity
        /// IspId is null if notification is sent to internal user
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        [AuthorizeUser(Permissions.SendNotifications)]
        [HttpPost("SendForActivity")]
        public async Task<bool> SendNotificationForActivity( SendNotificationForActivity notification)
        {
            return await _notificationService.SendNotificationForActivity(notification);
        }


        [HttpGet("Groups/{groupId}/SubscribedActivities")]
        public async Task<List<NotificationActivityTypeDto>> NotificationGroupSubscribedActivity(int groupId)
        {
            return await _notificationService.NotificationGroupSubscribedActivity(groupId);
        }

        [HttpGet("Groups/{groupId}/Subscribers")]
        public async Task<List<NotificationGroupSubscriberDto>> NotificationGroupSubscribers(int groupId)
        {
            return await _notificationService.NotificationGroupSubscribers(groupId);
        }
        [HttpGet("AppUsers/{appuserId}/SubscribedActivities")]
        public async Task<List<AppUserSubscribedActivityDto>> AppUserSubscribedActivities(int appuserId)
        {
            return await _notificationService.AppUserSubscribedActivities(appuserId);
        }
        #endregion
    }
}