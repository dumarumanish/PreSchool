using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class ForgotPasswordEmailViewModel : BaseEmailViewModel
    {
        public string Username { get;  }
        public string Code { get;  }
    
        public ForgotPasswordEmailViewModel(string username,string code,string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Code = code;
            Subject = "Recover your Hattiya account.";
        }
    }
}
