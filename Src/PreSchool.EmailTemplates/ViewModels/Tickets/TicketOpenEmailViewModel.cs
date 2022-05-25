using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates.ViewModels
{
    public class TicketOpenEmailViewModel : BaseEmailViewModel
    {
        public string UserName { get; set; }
        public string TicketId { get; }
        public DateTime TicketDate { get; set; }
        public string Detail { get; set; }
        public TicketOpenEmailViewModel(string userName,string ticketId,DateTime ticketDate,string detail, string toEmailId)
        {
            UserName = userName;
            TicketId = ticketId;
            TicketDate = ticketDate;
            Detail = detail;
            ToEmailId = toEmailId;
            Subject = "Your new ticket has been created "+TicketId+ " .";
        }

    }
}
