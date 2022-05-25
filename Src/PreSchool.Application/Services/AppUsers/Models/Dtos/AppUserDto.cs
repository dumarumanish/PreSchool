using PreSchool.Application.Services.Addresses.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Dtos
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Is email verified or not
        /// </summary>
        public bool IsEmailVerified { get; set; }
        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the appUser is required to re-login
        /// </summary>
        public bool RequireReLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating number of failed login attempts (wrong password)
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the date and time until which a appuser cannot login (locked out)
        /// </summary>
        public DateTime? CannotLoginUntilDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the appuser is active
        /// </summary>
        public bool IsActive { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }


        /// <summary>
        /// Primary address of the user
        /// </summary>
        public int? AddressId { get; set; }
        public AddressDto Address { get; set; }


        public List<RoleDto> AppUserRoles { get; set; }

    
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Store in which appuser is register
        /// </summary>
        public int? RegisteredInStoreId { get; set; }
        public string RegisteredInStore { get; set; }


        public int AppUserTypeId { get; set; }
        public string AppUserType { get; set; }
    }
}
