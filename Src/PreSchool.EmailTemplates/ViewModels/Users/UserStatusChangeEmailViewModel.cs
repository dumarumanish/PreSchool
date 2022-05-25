using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class UserStatusChangeEmailViewModel : BaseEmailViewModel
    {
        public string Username { get;  }
        public string UserStatus { get;  }
        public string Remark { get;  }


        public UserStatusChangeEmailViewModel(string username, string userStatus, string remark,string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            UserStatus = userStatus;
            Remark = remark;
            Subject = "Your user status is " + UserStatus;
        }
    }
}
