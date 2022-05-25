using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class BlogPublishedEmailViewModel : BaseEmailViewModel
    {
        public string Username { get;  }
        public string BlogTitle { get;  }
        public string BlogUrl { get;  }
    

        public BlogPublishedEmailViewModel(string username,string blogTitle,string blogUrl, string toEmailId)
        {
            Username = username;
            ToEmailId = toEmailId;
            BlogTitle = blogTitle;
            BlogUrl = blogUrl;
            Subject = "Congratulations, your blog has been published " ;
        }
    }
}
