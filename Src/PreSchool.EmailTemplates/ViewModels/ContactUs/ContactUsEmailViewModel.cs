using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class ContactUsEmailViewModel : BaseEmailViewModel
    {
        public string Username { get; }
        public ContactUsEmailViewModel(string username, string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Subject = "Thank you for contacting Hattiya.";
        }

    }
}
