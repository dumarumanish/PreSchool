using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class ResetPasswordEmailViewModel : BaseEmailViewModel
    {
        public string Username { get;  }
    
        public ResetPasswordEmailViewModel(string username, string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Subject = "Password reset successfully  ";
        }
    }
}
