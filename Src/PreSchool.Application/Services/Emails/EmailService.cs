using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.Emails.Models;
using PreSchool.Data;
using PreSchool.EmailTemplates;
using PreSchool.EmailTemplates.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;

namespace PreSchool.Application.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IRazorViewToStringRenderer _renderer;
        private readonly ICurrentUserService _currentUserService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly EmailSettings _emailSettings;
        private readonly IDateTime _dateTime;

        public EmailService(IRazorViewToStringRenderer renderer,
                            ICurrentUserService currentUserService,
                            IServiceScopeFactory scopeFactory,
            IOptions<EmailSettings> emailSettings,
                            IDateTime dateTime

            )
        {
            _renderer = renderer;
            _currentUserService = currentUserService;
            _scopeFactory = scopeFactory;
            _emailSettings = emailSettings.Value;
            _dateTime = dateTime;
        }

        public async void SendEmail(EmailModel model)
        {
            if (model == null)
                throw new BadRequestException("Email is required");

            var emailTemplate = Templates.HtmlEmail;
            var body = await _renderer.RenderViewToStringAsync(emailTemplate, new EmailViewModel
            {
                Body = model.Body,
                Title = model.Title
            });

            SendEmailAsync(model.EmailId, model.Subject, body);
        }

        #region AppUser

        public async void WelcomeCustomer(WelcomeCustomerEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.WelcomeCustomer;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body, true);

        }
        public async void CustomerVerified(CustomerVerifiedEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.CustomerVerified;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }
        public async void WelcomeInternalUser(WelcomeInternalUserEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.WelcomeInternalUser;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body, true);

        }




        public async void ForgotPassword(ForgotPasswordEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.ForgotPassword;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }

        public async void ResendVerificationCode(ResendVerificationCodeEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.ResendVerificationCode;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }
        public async void ResetPassword(ResetPasswordEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.ResetPassword;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }

        public async void UserStatusChange(UserStatusChangeEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.UserStatusChange;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }
        public async void UserActiveStatus(UserActiveStatusEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.UserActiveStatus;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }
        public async void WelcomeCustomerOfSocialMedia(WelcomeCustomerOfSocialMediaEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.WelcomeCustomerOfSocialMedia;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body, true);

        }

        public async void CustomerEmailVerifyWelcome(CustomerEmailVerifyWelcomeEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.CustomerEmailVerifyWelcome;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body, true);

        }

        #endregion

        public async void BlogPublished(BlogPublishedEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.BlogPublished;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }

        #region FeedBack
        public async void FeedbackResponse(FeedbackResponseEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.FeedbackResponse;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }

        public async void FeedbackAcknowledge(FeedbackAcknowledgeEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.FeedbackAcknowledge;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }


        #endregion

        #region Subscriptions
        public async void Subscribed(SubscribedEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.Subscribed;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }
        #endregion

        #region ContactUs
        public async void ContactUs(ContactUsEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.ContactUs;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }
        #endregion

        #region Tickets
        public async void TicketOpen(TicketOpenEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.TicketOpen;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }

        public async void TicketReply(TicketReplyEmailViewModel model)
        {
            if (model == null)
                return;
            var template = Templates.TicketReply;
            var body = await _renderer.RenderViewToStringAsync(template, model);
            SendEmailAsync(model.ToEmailId, model.Subject, body);

        }
        #endregion

        #region email helper methods



        private async void SendEmailAsync(string email, string subject, string body, bool fromWelcomeEmail = false)
        {

            // Save the history to database
            if (string.IsNullOrWhiteSpace(email))
                return;

            int? currentUserId = null;
            if (_currentUserService.IsAuthenticated)
                currentUserId = _currentUserService.AppUserId;


            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                context.EmailHistories.Add(new Data.Entities.Emails.EmailHistory
                {
                    Body = body,
                    SendByAppUser = true,
                    SendDateTime = _dateTime.Now,
                    SenderId = currentUserId,
                    Subject = subject,
                    ToEmailId = email
                });
                await context.SaveChangesAsync();

            }

            try
            {
                var mimeMessage = new MimeMessage();
                if (fromWelcomeEmail)
                    mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.WelcomeSender));
                else
                    mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                //  mimeMessage.To.Add(new MailboxAddress(email));
                mimeMessage.To.Add(MailboxAddress.Parse(email));
                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = body ?? ""
                };
                //send mail to bcc.
                if (!string.IsNullOrEmpty(_emailSettings.BCCEmailAddress))
                    mimeMessage.Bcc.Add(MailboxAddress.Parse(_emailSettings.BCCEmailAddress));

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
                    if (fromWelcomeEmail)
                        await client.AuthenticateAsync(_emailSettings.WelcomeSender, _emailSettings.WelcomeSenderPassword);
                    else
                        await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }


        private async void SendEmailAsync(List<string> emails, string subject, string body)
        {
            int? currentUserId = null;
            if (_currentUserService.IsAuthenticated)
                currentUserId = _currentUserService.AppUserId;

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                // Save the history to database
                foreach (var email in emails)
                {
                    context.EmailHistories.AddRange(new Data.Entities.Emails.EmailHistory
                    {
                        Body = body,
                        SendByAppUser = true,
                        SendDateTime = _dateTime.Now,
                        SenderId = currentUserId,
                        Subject = subject,
                        ToEmailId = email
                    });
                }
                await context.SaveChangesAsync();

            }


            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                var emailLists = new InternetAddressList();
                foreach (var email in emails)
                {
                    emailLists.Add(new MailboxAddress(email));
                }
                mimeMessage.To.AddRange(emailLists);

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = body
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
                throw new InvalidOperationException(ex.Message);
            }
        }

        private async void SendEmailAsync(string email, string subject, string body, List<EmailAttachment> attachments)
        {
            int? currentUserId = null;
            if (_currentUserService.IsAuthenticated)
                currentUserId = _currentUserService.AppUserId;

            // Save the history to database

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                context.EmailHistories.Add(new Data.Entities.Emails.EmailHistory
                {
                    Body = body,
                    SendByAppUser = true,
                    SendDateTime = _dateTime.Now,
                    SenderId = currentUserId,
                    Subject = subject,
                    ToEmailId = email
                });
                await context.SaveChangesAsync();

            }



            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = body;

                foreach (var attachment in attachments)
                {
                    //// Remove illegal character form filename
                    //string illegal = "\"M\"\\a/ry/ h**ad:>> a\\/:*?\"| li*tt|le|| la\"mb.?";
                    //Regex r = new Regex(illegal);
                    //var fileName = r.Replace(attachment.FileName, "");

                    //// Limit file name to 50
                    //fileName = !String.IsNullOrWhiteSpace(fileName) && fileName.Length >= 50
                    //                  ? fileName.Substring(0, 50)
                    //                  : fileName;

                    var fileName = attachment.FileName;
                    builder.Attachments.Add(fileName, attachment.File);
                }

                mimeMessage.Body = builder.ToMessageBody();

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
                throw new InvalidOperationException(ex.Message);
            }
        }
        #endregion

    }
}
