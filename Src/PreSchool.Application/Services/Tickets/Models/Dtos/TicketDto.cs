using PreSchool.Data.Entities.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }

        
        public string TicketNumber { get; set; }

        /// <summary>
        /// Id of the user if logined, who generated the ticket
        /// </summary>
        public int? AppUserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }

        /// <summary>
        /// Id of the department who is responsible to handel the ticket
        /// </summary>
        public int DepartmentId { get; set; }
        public string Department { get; set; }

        /// <summary>
        /// Id of the service provided by department for which user has generated the ticket
        /// May or may not required
        /// </summary>
        public int? DepartmentServiceId { get; set; }
        public string DepartmentService { get; set; }

        public TicketPriorityEnum PriorityId { get; set; }
        public string Priority { get; set; }
        public TicketStatusEnum StatusId { get; set; }
        public string Status { get; set; }

        public string Message { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string LastReplier { get; set; }
        public DateTime? LastRepliedOn { get; set; }

        public List<TicketAttachmentDto> Attachments { get; set; }
        public List<TicketReplyDto> Replies { get; set; }

    }
}
