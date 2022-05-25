using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Departments;
using System;
using System.Collections.Generic;

namespace PreSchool.Data.Entities.Tickets
{
    public class Ticket : CommonProperties
    {
        

        /// <summary>
        /// Tickent code
        /// </summary>
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

        /// <summary>
        /// Id of the service provided by department for which user has generated the ticket
        /// May or may not required
        /// </summary>
        public int? DepartmentServiceId { get; set; }

        public TicketPriorityEnum PriorityId { get; set; }
        public TicketStatusEnum StatusId { get; set; }

        public string Message { get; set; }

      
        public string LastReplier { get; set; }
        public DateTime? LastRepliedOn { get; set; }
        public virtual AppUserType AppUserType { get; set; }


        public virtual DepartmentService DepartmentService { get; set; }


        public virtual Department Department { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }


        public virtual ICollection<TicketReply> TicketReplies { get; set; }

        public virtual ICollection<TicketUser> TicketUsers { get; set; }


        public Ticket()
        {
            TicketUsers = new HashSet<TicketUser>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketReplies = new HashSet<TicketReply>();

        }
    }
}
