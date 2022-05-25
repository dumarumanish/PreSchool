using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Commands
{
    public class AddRemoveTicketUser
    {
        public int TicketId { get; set; }
        public int AppUserId { get; set; }
    }
}
