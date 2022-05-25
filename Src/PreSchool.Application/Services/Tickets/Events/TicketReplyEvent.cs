using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Events
{
    public class TicketReplyEvent
    {
        public TicketReplyEvent(Ticket ticket,string message)
        {

            Ticket = ticket;
            Message = message;
        }

        public Ticket Ticket { get; }
        public string Message { get; }

    }
}
