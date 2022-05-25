using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class TicketReplyEmailViewModel : BaseEmailViewModel
    {
        public string UserName { get; set; }
        public string TicketId { get; }
        public DateTime TicketDate { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }

        public TicketReplyEmailViewModel(string userName,string ticketId,DateTime ticketDate
            ,string comment,string status, string toEmailId)
        {
            UserName = userName;
            TicketId = ticketId;
            TicketDate = ticketDate;
            Comment = comment;
            Status = status;
            ToEmailId = toEmailId;
            Subject = "Your ticket for  " + TicketId+ " has been updated.";
        }

    }
}
