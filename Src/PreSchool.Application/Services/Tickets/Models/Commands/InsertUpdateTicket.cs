using PreSchool.Data.Entities.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Commands
{
   public  class InsertUpdateTicket: ICaptchaModel
    {
        public int Id { get; set; }
      
     
        
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// Id of the department who is responsible to handel the ticket
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Id of the service provided by department for which user has generated the ticket
        /// May or may not required
        /// </summary>
        public int? DepartmentServiceId { get; set; }

        public TicketPriorityEnum PriorityId { get; set; }

        [Required]
        public string Message { get; set; }


    }
}
