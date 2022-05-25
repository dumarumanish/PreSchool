using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Dtos
{
    public class TicketAttachmentDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string AttachmentName { get; set; }

        public string AttachmentLocation { get; set; }
    }
}
