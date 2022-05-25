using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Models.AppUsers.Queries
{
    public class CustomerLogin 
    {
        
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
