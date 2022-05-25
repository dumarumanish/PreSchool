using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class WelcomeCustomerOfSocialMediaEmailViewModel : BaseEmailViewModel
    {

        public string Username { get;  }
        public WelcomeCustomerOfSocialMediaEmailViewModel(string username, string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Subject = "Welcome to Hattiya";
        }

    }
}
