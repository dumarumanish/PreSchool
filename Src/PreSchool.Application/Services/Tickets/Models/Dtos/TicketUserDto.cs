using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Dtos
{
    public class TicketUserDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public TicketListDto Ticket { get; set; }
        public int AppUserId { get; set; }
        public string AppUsername { get; set; }
        public DateTime AssignedOn { get; set; }
    }
}
