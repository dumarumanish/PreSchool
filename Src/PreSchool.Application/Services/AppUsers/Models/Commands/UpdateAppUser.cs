using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class UpdateAppUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public InsertUpdateAddress Address { get; set; }

        /// <summary>
        /// Admin Comment for the user
        /// </summary>
        public string AdminComment { get; set; }

        //public List<InsertUpdateInternalUserAcademic> InternalUserAcademics { get; set; }
        //public List<InsertUpdateInternalUserDocuments> InternalUserDocuments { get; set; }
        //public List<InsertUpdateInternalUserEmergencyContact> InternalUserEmergencyContacts { get; set; }
        //public List<InsertUpdateInternalUserExperience> InternalUserExperiences { get; set; }



    }
}
