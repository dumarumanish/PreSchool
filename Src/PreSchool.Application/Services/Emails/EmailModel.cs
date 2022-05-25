using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Emails.Models
{
    public class EmailModel
    {
        public string EmailId { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
