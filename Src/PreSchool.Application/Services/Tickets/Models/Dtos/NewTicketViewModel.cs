using PreSchool.Application.HelperClasses;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Dtos
{
    public class NewTicketViewModel
    {
        public List<EnumValue> TicketPriorities { get; set; }
        public List<EnumValue> TicketStatuses { get; set; }
    }
}
