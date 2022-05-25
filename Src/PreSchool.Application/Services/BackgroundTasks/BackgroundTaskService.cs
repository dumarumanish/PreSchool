using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.Notifications;
using PreSchool.Application.Services.Notifications.Models.Dtos;
using PreSchool.Application.Services.SMSSenders;
using Hangfire;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.BackgroundTasks
{
    public class BackgroundTaskService : IBackgroundTaskService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubNotificationHelper _hubNotificationHelper;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly EmailSettings _emailSettings;
        private readonly AppSettings _appSettings;

        public BackgroundTaskService(
            IServiceScopeFactory scopeFactory,
            IOptions<EmailSettings> emailSettings,
            IOptions<AppSettings> appSettings,
            IHubNotificationHelper hubNotificationHelper,
            IBackgroundJobClient backgroundJobClient


            )
        {
            _scopeFactory = scopeFactory;
            _hubNotificationHelper = hubNotificationHelper;
            _backgroundJobClient = backgroundJobClient;
            _emailSettings = emailSettings.Value;
            _appSettings = appSettings.Value;

        }
        public void FireAndForgotTask(Task task)
        {
            _backgroundJobClient.Enqueue(() => task);
        }

        public void DelayedTask(Task task, int delayInMinutes)
        {
            _backgroundJobClient.Schedule(() => task, TimeSpan.FromMinutes(delayInMinutes));
        }


        public async void SendUnSentNotifications()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                var unsentNotifications = await context.Notifications
                    .Where(n => !n.IsSent)
                    .ToListAsync();


                // Emails
                var unsentEmails = unsentNotifications
                    .Where(n => n.NotificationTypeId == Data.Entities.Notifications.NotificationTypeEnum.Email);

                if (unsentEmails != null && unsentEmails.Count() > 0)
                {
                    foreach (var notification in unsentEmails)
                        _backgroundJobClient.Enqueue(() => SendEmailNotification(notification.Id));

                }

                // PushNotification
                var unsentPushNotifications = unsentNotifications
                   .Where(n => n.NotificationTypeId == Data.Entities.Notifications.NotificationTypeEnum.Email);

                if (unsentPushNotifications != null && unsentPushNotifications.Count() > 0)
                {
                    foreach (var notification in unsentPushNotifications)
                        _backgroundJobClient.Enqueue(() => SendPushNotification(notification.Id));

                }

                // SMS
                var unsentSms = unsentNotifications
                                   .Where(n => n.NotificationTypeId == Data.Entities.Notifications.NotificationTypeEnum.Mobile);

                if (unsentSms != null && unsentSms.Count() > 0)
                {
                    foreach (var notification in unsentSms)
                        _backgroundJobClient.Enqueue(() => SendSMSNotification(notification.Id));


                }
            }
        }

        public async Task SendEmailNotification(int notificationId)
        {
            // Default email setting

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                var notification = await context.Notifications
                    .FirstOrDefaultAsync(n => n.Id == notificationId);

                if (notification == null)
                    return;





                try
                {
                    // Replace email header 
                    var body = notification.Message;

                    var mimeMessage = new MimeMessage();

                    mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));


                    //  mimeMessage.To.Add(new MailboxAddress(email));
                    mimeMessage.To.Add(MailboxAddress.Parse(notification.Email));
                    mimeMessage.Subject = notification.Title;

                    mimeMessage.Body = new TextPart("html")
                    {
                        Text = body ?? ""
                    };

                    using (var client = new SmtpClient())
                    {
                        // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        //  if (_env.IsDevelopment())
                        {
                            // The third parameter is useSSL (true if the client should make an SSL-wrapped
                            // connection to the server; otherwise, false).
                            await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, true);

                        }
                        //else
                        //{
                        //    await client.ConnectAsync(_emailSettings.MailServer);
                        //}

                        // Note: only needed if the SMTP server requires authentication
                        await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                        await client.SendAsync(mimeMessage);

                        await client.DisconnectAsync(true);
                    }

                }
                catch (Exception ex)
                {
                    // TODO: handle exception
                    Logger.Error(ex);
                }

                notification.IsSent = true;
                await context.SaveChangesAsync();
            }
        }

        public async Task SendPushNotification(int notificationId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                var notification = await context.Notifications
                    .Include(n => n.ActivityType)
                    .FirstOrDefaultAsync(n => n.Id == notificationId);

                if (notification == null)
                    return;

                if (notification.RecipientId == null || notification.RecipientId == 0)
                    return;

                var notificationDto = new NotificationDto
                {
                    Id = notification.Id,
                    ActivityType = new NotificationActivityTypeDto
                    {
                        Id = notification.ActivityType.Id,
                        Name = notification.ActivityType.Name,
                        RedirectUrl = notification.ActivityType.RedirectUrl
                    },
                    ActivityTypeId = notification.ActivityTypeId,
                    DeliveredDate = notification.DeliveredDate,
                    Message = notification.Message,
                    ReadDate = notification.ReadDate,
                    RecipientId = notification.RecipientId ?? 0,
                    SenderId = notification.SenderId,
                    SentDate = notification.SentDate,
                    SourceEntityId = notification.SourceEntityId,
                    Title = notification.Title,
                };

                await _hubNotificationHelper.SendNotificationParallel(notification.RecipientId ?? 0, JsonConvert.SerializeObject(notificationDto));
            }
        }

        public async Task SendSMSNotification(int notificationId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var smsSenderService = scope.ServiceProvider.GetRequiredService<ISMSSenderService>();

                var notification = await context.Notifications
                    .Include(n => n.ActivityType)
                    .FirstOrDefaultAsync(n => n.Id == notificationId);

                if (notification == null)
                    return;

                if (string.IsNullOrWhiteSpace(notification.PhoneNumber))
                    return;

                if (await smsSenderService.SendSMS(notification.PhoneNumber, notification.Message))
                {
                    notification.IsSent = true;
                    await context.SaveChangesAsync();
                }

            }
        }


    }
}
