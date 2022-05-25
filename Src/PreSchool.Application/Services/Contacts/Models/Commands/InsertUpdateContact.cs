using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Contacts.Models.Commands
{
    public class InsertUpdateContact : ICaptchaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "IssueType is required"), Range(1, int.MaxValue, ErrorMessage = "IssueTypeId is required")]

        public int IssueTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }

    }
}
