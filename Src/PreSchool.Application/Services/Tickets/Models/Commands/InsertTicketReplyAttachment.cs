using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Commands
{
   public  class InsertTicketReplyAttachment
    {
        public int ticketId { get; set; }
        public int ticketReplyId { get; set; }
        public string attachmentName { get; set; }

        public IFormFile attachment { get; set; }
    }
}
