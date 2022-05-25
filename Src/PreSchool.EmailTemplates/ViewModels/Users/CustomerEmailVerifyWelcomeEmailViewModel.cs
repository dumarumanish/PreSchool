using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class CustomerEmailVerifyWelcomeEmailViewModel : BaseEmailViewModel
    {

        public string Username { get; set; }
        public string Link { get; set; }

        public CustomerEmailVerifyWelcomeEmailViewModel(string username,string link, string toEmailId)
        {
            Username = username;
            Link = link;
            ToEmailId = toEmailId;
            Subject = "Welcome to Hattiya";
        }

    }
}
