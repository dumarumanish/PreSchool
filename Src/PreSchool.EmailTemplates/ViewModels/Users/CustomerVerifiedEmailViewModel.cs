using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class CustomerVerifiedEmailViewModel : BaseEmailViewModel
    {

        public string Username { get;  }
        public CustomerVerifiedEmailViewModel(string username, string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Subject = "Verified.";
        }

    }
}
