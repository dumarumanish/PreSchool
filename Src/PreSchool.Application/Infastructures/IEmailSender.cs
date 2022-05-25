using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PreSchool.Application.Models;

namespace PreSchool.Application.Infastructures
{
    public interface IEmailSender
    {
        void  SendEmailAsync(string emailId, string sbject, string message);
        void SendEmailAsync(List<string> emails, string subject, string message);
        void SendEmailAsync(string email, string subject, string message, List<EmailAttachment> attachments);
    }
}
