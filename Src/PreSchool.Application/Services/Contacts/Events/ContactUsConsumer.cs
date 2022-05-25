using Newtonsoft.Json;
using PreSchool.Application.Events;
using PreSchool.Application.Services.Emails;
using PreSchool.EmailTemplates;
using PreSchool.Application.Infastructures;
using PreSchool.EmailTemplates.ViewModels;
using PreSchool.Application.Services.Notifications;
using PreSchool.Data.Entities.Notifications;

namespace PreSchool.Application.Services.Contacts.Events
{
    public class ContactUsConsumer : IEventConsumer<ContactUsEvent> 


    {
        private readonly IEmailService _emailService;
            private readonly IRazorViewToStringRenderer _renderer;
            private readonly ICurrentUserService _currentUserService;
        private readonly INotificationService _notificationService;

        public ContactUsConsumer(
                IEmailService emailService,
                IRazorViewToStringRenderer renderer,
                  ICurrentUserService currentUserService,
            INotificationService notificationService



                )
        {
                _emailService = emailService;
                _renderer = renderer;
                _currentUserService = currentUserService;
            _notificationService = notificationService;


        }


        public async void HandleEvent(ContactUsEvent eventMessage, EventSender sender, object parameter = null)
        {
            //// For activity log
            //Logger.Activity(NotificationActivityTypeEnum.ContactUs.ToNameString(), NotificationActivityTypeEnum.ContactUs, sender, parameter);

            var emailVm = new ContactUsEmailViewModel(
                          eventMessage.UserName,            
                          eventMessage.Email
                          );

            _emailService.ContactUs(emailVm);

            //// Activity notification
            //var sendNotificationForActivity = new SendNotificationForActivity()
            //{
            //    EmailNotification = new NotificationMessage
            //    {
            //        Title = "ContactUs",
            //        Message = $"ContactUs : {eventMessage.Email}"
            //    },
            //    SMSNotification = new NotificationMessage
            //    {
            //        Title = "ContactUs",
            //        Message = $"ContactUs : {eventMessage.Email}"
            //    },

            //    PushNotification = new NotificationMessage
            //    {
            //        Title = "ContactUs",
            //        Message = $"ContactUs : {eventMessage.Email}"
            //    },
            //    NotificationActivityTypeId = NotificationActivityTypeEnum.ContactUs,
            //    SourceEntityId = eventMessage.Id,
            //    SenderId = sender?.AppUserId,
            //};


            //await _notificationService.SendNotificationForActivity(sendNotificationForActivity);
        }

    }


}
