using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Tickets
{
    public class TicketUser : CommonProperties
    {
        public int TicketId { get; set; }
        public int AppUserId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
