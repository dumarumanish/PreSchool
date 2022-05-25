using PreSchool.Data.Entities.AppUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Tickets
{
    public class TicketReply : CommonProperties
    {
        public int TicketId { get; set; }

        public int? AppUserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<TicketReplyAttachment> TicketReplyAttachments { get; set; }


        public TicketReply()
        {
            TicketReplyAttachments = new HashSet<TicketReplyAttachment>();

        }
    }
}
