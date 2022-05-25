using PreSchool.Data.Entities.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Commands
{
    public class ChangeTicketStatus
    {
        public int TicketId { get; set; }
        public TicketStatusEnum TicketStatusId { get; set; }

    }
}
