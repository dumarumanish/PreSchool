using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Contacts.Models.Commands
{
    public class AddressContact
    {
        [Required(ErrorMessage = "Contact is required"), Range(1, int.MaxValue, ErrorMessage = "ContactId is required")]

        public int ContactId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
