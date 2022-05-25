using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Commands
{
    public class InsertStoreUser
    {
        [Range(1, int.MaxValue, ErrorMessage = "Store is required")]
        [Required]
        public int StoreId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public string JobTitle { get; set; }
        public InsertUpdateAddress Address { get; set; }

        /// <summary>
        /// Admin Comment for the user
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Required to change the passowrd
        /// </summary>
        public bool RequiredPasswordChange { get; set; }

        /// <summary>
        /// Indicating whether the appUser must change passwords after a specified time
        /// </summary>
        public bool EnablePasswordLifetime { get; set; }

        /// <summary>
        /// If EnablePasswordLifetime then password expiry date
        /// </summary>
        public DateTime? PasswordExpiredOn { get; set; }

        /// <summary>
        ///Appuser cannot login until date (locked out)
        /// </summary>
        public DateTime? CannotLoginUntilDate { get; set; }

        /// <summary>
        /// Roles of the appuser
        /// </summary>
        [Required(ErrorMessage = "Atleast one role is required")]
        public List<int> RoleIds { get; set; }


    }
}
