using Newtonsoft.Json;
using PreSchool.Application.Events;
using PreSchool.Application.Services.Notifications.Models.Dtos;
using PreSchool.Data.Entities.Notifications.Events;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Notifications
{
    public class NotificationEventConsumer : IEventConsumer<NewNotificationReceivedEvent>
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IHubNotificationHelper _hubNotificationHelper;

        public NotificationEventConsumer(IConnectionManager connectionManager, IHubNotificationHelper hubNotificationHelper)
        {
            _connectionManager = connectionManager;
            _hubNotificationHelper = hubNotificationHelper;
        }

        public void HandleEvent(NewNotificationReceivedEvent eventMessage, EventSender sender, object parameter = null)
        {
            var notification = new NotificationDto
            {
                Id = eventMessage.Notification.Id,
                ActivityType = new NotificationActivityTypeDto
                {
                    Id = eventMessage.NotificationActivityType.Id,
                    Name = eventMessage.NotificationActivityType.Name,
                    RedirectUrl = eventMessage.NotificationActivityType.RedirectUrl
                },
                ActivityTypeId = eventMessage.Notification.ActivityTypeId,
                DeliveredDate = eventMessage.Notification.DeliveredDate,
                Message = eventMessage.Notification.Message,
                ReadDate = eventMessage.Notification.ReadDate,
                RecipientId = eventMessage.Notification.RecipientId,
                SenderId = eventMessage.Notification.SenderId,
                SentDate = eventMessage.Notification.SentDate,
                SourceEntityId = eventMessage.Notification.SourceEntityId,
                Title = eventMessage.Notification.Title,
            };
            _hubNotificationHelper.SendNotificationParallel(notification.RecipientId ?? 0, JsonConvert.SerializeObject(notification));

        }
    }


}
