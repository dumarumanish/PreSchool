using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class SubscribedEmailViewModel : BaseEmailViewModel
    {
        public string Username { get; }
        public SubscribedEmailViewModel(string username, string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Subject = "Thank you for your subscription.";
        }

    }
}
