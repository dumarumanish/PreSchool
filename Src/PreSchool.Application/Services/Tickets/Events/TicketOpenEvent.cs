using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Events
{
    public class TicketOpenEvent
    {
        public TicketOpenEvent(Ticket ticket)
        {

            Ticket = ticket;
        }

        public Ticket Ticket { get; }
    }
}
