using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class UserActiveStatusEmailViewModel : BaseEmailViewModel
    {
        public string Username { get;  }
        public string Remark { get;  }

        public UserActiveStatusEmailViewModel(string username, string remark,string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            Remark = remark;
            Subject = "Your user status is Active" ;
        }
    }
}
