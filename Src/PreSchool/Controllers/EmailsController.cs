using PreSchool.Application.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;
namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly EmailSettings _emailSettings;

        public EmailsController(
            IOptions<EmailSettings> emailSettings
            )
        {
            _emailSettings = emailSettings.Value;
        }

        [HttpPost]
        public async Task<bool> SendEmail(EmailModel email)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                mimeMessage.To.Add(MailboxAddress.Parse(email.EmailId));

                mimeMessage.Subject = email.Subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = email.Body ?? ""
                };
                //send mail to bcc.
                mimeMessage.Bcc.Add(MailboxAddress.Parse("zaakstest@gmail.com"));

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
                return true;

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw ex;
            }
        }
    }

    public class EmailModel
    {
        public string EmailId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}