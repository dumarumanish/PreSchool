using PreSchool.Data.Entities.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Tickets
{
    public class TicketReplyAttachment : CommonProperties
    {
        public int TicketReplyId { get; set; }
        public string AttachmentName { get; set; }
        public int FileId { get; set; }
        public virtual File File { get; set; }
        public virtual TicketReply TicketReply { get; set; }
    }
}
