using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Dtos
{
    public class AppUserListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Is email verified or not
        /// </summary>
        public bool IsEmailVerified { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }


        /// <summary>
        /// Type of app user
        /// </summary>
        public int AppUserTypeId { get; set; }
        public string AppUserType { get; set; }
        public bool IsActive { get; set; }



    }
}
