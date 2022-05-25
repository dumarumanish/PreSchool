using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class WelcomeInternalUserEmailViewModel : BaseEmailViewModel
    {

        public string Username { get;  }
        public WelcomeInternalUserEmailViewModel(string username, string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Subject = "Welcome ";
        }

    }
}
