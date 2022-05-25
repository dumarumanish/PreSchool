using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Dtos
{
    public class TicketReplyDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int? AppUserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<TicketReplyAttachmentDto> ReplyAttachments { get; set; }
        public TicketReplyDto()
        {

        }
    }
}
