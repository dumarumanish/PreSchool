using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Commands
{
    public class InsertUpdateTicketReply
    {
        public int Id { get; set; }
        
        public int TicketId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required, MaxLength(1024)]
        public string Message { get; set; }
    }
}
