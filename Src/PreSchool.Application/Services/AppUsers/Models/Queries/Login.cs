using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Models.AppUsers.Queries
{
    public class Login 
    {
        /// <summary>
        /// If the user is store user
        /// </summary>
        public int? StoreId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
