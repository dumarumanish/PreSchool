using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class WelcomeCustomerEmailViewModel : BaseEmailViewModel
    {

        public string Username { get;  }
        public string OTPCode { get; set; }
        public WelcomeCustomerEmailViewModel(string username,string oTPCode, string toEmailId)
        {
            Username = username;
            OTPCode = oTPCode;
            ToEmailId = toEmailId;
            Subject = "Welcome to Hattiya";
        }

    }
}
