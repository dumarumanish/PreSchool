using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class ResendVerificationCodeEmailViewModel : BaseEmailViewModel
    {
        public string Username { get;  }
        public string Code { get;  }
    
        public ResendVerificationCodeEmailViewModel(string username,string code,string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Code = code;
            Subject = "Resend verification code.  ";
        }
    }
}
