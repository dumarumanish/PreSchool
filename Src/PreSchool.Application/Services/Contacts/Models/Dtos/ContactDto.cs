using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Contacts.Models.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public int IssueTypeId { get; set; }
        public string IssueName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Message { get; set; }
        public bool IsAddressed { get; set; }
        public string Subject { get; set; }
        public string RepliedSubject { get; set; }
        public string RepliedMessage { get; set; }
        public DateTime? ResponseDate { get; set; }

    }
}
