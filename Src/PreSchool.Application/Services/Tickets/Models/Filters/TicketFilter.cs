using PreSchool.Application.Models;
using PreSchool.Data.Entities.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Tickets.Models.Filters
{
    public class TicketFilter : PaginationFilter
    {
        public string Search { get; set; }
        public int? DepartmentId { get; set; }
        public int? DepartmentServiceId { get; set; }
        public TicketPriorityEnum? PriorityId { get; set; }
        public TicketStatusEnum? StatusId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
