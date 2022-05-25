using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class FeedbackResponseEmailViewModel : BaseEmailViewModel
    {
        public string Username { get;  }
        public string FormatedDate { get;  }
        public string Remark { get;  }

        public FeedbackResponseEmailViewModel(string username, string formatedDate,string remark, string toEmailId)
        {

            Username = username;
            ToEmailId = toEmailId;
            FormatedDate = formatedDate;
            Remark = remark;

            Subject = "Response for your query ";
        }
    }
}
