using PreSchool.Data.Entities.AppUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Emails
{
    public class EmailHistory :BaseEntity
    {
        public string ToEmailId { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }
     
        public bool SendByAppUser { get; set; }
        public int? SenderId { get; set; }
        public DateTime SendDateTime { get; set; }
        public virtual AppUser Sender { get; set; }
    }
}
