using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class FeedbackAcknowledgeEmailViewModel : BaseEmailViewModel
    {
        public string Username { get;  }

        public FeedbackAcknowledgeEmailViewModel(string username, string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Subject = "Thank you for contacting us. ";
        }
    }
}
