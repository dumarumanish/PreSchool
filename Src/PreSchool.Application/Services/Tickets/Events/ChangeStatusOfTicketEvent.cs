using PreSchool.Data.Entities.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Events
{
    public class ChangeStatusOfTicketEvent
    {
        public ChangeStatusOfTicketEvent(Ticket ticket)
        {
            Ticket = ticket;
        }

        public Ticket Ticket { get; }

    }
}
