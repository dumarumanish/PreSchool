using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class BaseEmailViewModel
    {
        public string ToEmailId { get;  set; }
        public string Subject { get; protected set; }
        public string SupportLink { get; private set; }
        public string WebSiteLink { get; private set; }
        public string ClientBaseUrl { get; private set; }

        public BaseEmailViewModel()
        {
            SupportLink = "support@hattiya.com";
            WebSiteLink = "www.hattiya.com";
            //for test url
            ClientBaseUrl = "https://PreSchool.krennovatech.net/register-login";
        }
    }
}
