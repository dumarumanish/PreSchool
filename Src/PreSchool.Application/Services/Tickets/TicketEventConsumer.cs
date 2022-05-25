//using PreSchool.Application.Events;
//using PreSchool.Application.Services.Emails;
//using PreSchool.Application.Services.Notifications;
//using PreSchool.Application.Services.Notifications.Models.Commands;
//using PreSchool.Application.Services.Tickets.Events;
//using PreSchool.Data.Entities.AppUsers;
//using PreSchool.Data.Entities.Notifications;
//using PreSchool.Data.Entities.Tickets;
//using PreSchool.EmailTemplates.ViewModels;

//namespace PreSchool.Application.Services.Customers
//{
//    public class TicketEventConsumer : IEventConsumer<TicketRegisteredEvent>
//         ,IEventConsumer<TicketPostEvent>
//         ,IEventConsumer<ChangeStatusOfTicketEvent>
//    {
//        private readonly IEmailService _emailService;
//        private readonly INotificationService _notificationService;

//        public TicketEventConsumer(IEmailService emailService,
//            INotificationService notificationService
//            )
//        {
//            _emailService = emailService;
//            _notificationService = notificationService;
//        }

//        public void HandleEvent(TicketRegisteredEvent eventMessage, EventSender sender, object parameter = null)
//        {
//            var sendNotificationForActivity = new SendNotificationForActivity()
//            {
//                Title = $"New ticket submitted.",
//                Message = $"Ticket number : {eventMessage.Ticket.TicketNumber}",
//                EmailTemplate = "",
//                NotificationActivityTypeId = NotificationActivityTypeEnum.TicketSubmitted,
//                SourceEntityId = eventMessage.Ticket.Id,
//            };

//            _notificationService.SendNotificationForActivity( sendNotificationForActivity);
//        }

//        public async void HandleEvent(TicketPostEvent eventMessage, EventSender sender, object parameter = null)
//        {
//            #region email and notification for client.
//            #region email send for external user
//            // Email.

//            var ticketPostEmailViewModel = new TicketPostEmailViewModel(
//               eventMessage.Ticket.AppUser.FirstName,
//               eventMessage.Ticket.TicketNumber,
//               eventMessage.Ticket.CreatedOn,
//               eventMessage.Ticket.Message,
//               eventMessage.Ticket.AppUser.EmailAddress

//              );

//            _emailService.TicketPost(ticketPostEmailViewModel);

//            #endregion

//            #region externalUser notification
//            // Generate email template for the notification
//            var sendNotification = new InsertNotificationCommand()
//            {
//                Title = $"Ticket Created",
//                Message = $"Your new ticket has been created.",
//                SourceEntityId = eventMessage.Ticket.Id,
//                ActivityTypeId = NotificationActivityTypeEnum.PostTicket,
//                RecipientId = eventMessage.Ticket.AppUser.Id,
//                //SenderId = _currentUserService.AppUserId,
//            };

//            await _notificationService.SendPushNotification(sendNotification);
//            #endregion

//            #endregion

//        }

//        public async void HandleEvent(ChangeStatusOfTicketEvent eventMessage, EventSender sender, object parameter = null)
//        {
//            #region email and notification for client.
//            #region email send for external user
//            // Email.

//            var ticketStatus = eventMessage.Ticket.StatusId == TicketStatusEnum.Open ? "Open"
//                :"Closed";

//            var changeStatusOfTicketEmailViewModel = new ChangeStatusOfTicketEmailViewModel(
//               eventMessage.Ticket.AppUser.FirstName,
//               eventMessage.Ticket.TicketNumber,
//               eventMessage.Ticket.CreatedOn,
//               eventMessage.Ticket.Message,
//               ticketStatus,
//                eventMessage.Ticket.AppUser.EmailAddress

//              );

//            _emailService.ChangeStatusOfTicket (changeStatusOfTicketEmailViewModel);

//            #endregion

//            #region externalUser notification
//            // Generate email template for the notification
//            var sendNotification = new InsertNotificationCommand()
//            {
//                Title = $"Ticket Created",
//                Message = $"Your new ticket has been created.",
//                SourceEntityId = eventMessage.Ticket.Id,
//                ActivityTypeId = NotificationActivityTypeEnum.TicketStatusChange,
//                RecipientId = eventMessage.Ticket.AppUser.Id,
//                //SenderId = _currentUserService.AppUserId,
//            };

//            await _notificationService.SendPushNotification(sendNotification);
//            #endregion

//            #endregion

//        }

//    }
//}
