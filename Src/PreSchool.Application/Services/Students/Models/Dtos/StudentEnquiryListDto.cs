using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Dtos
{
    public class StudentEnquiryListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public GenderEnum GenderId { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirthAD { get; set; }
        public string DateOfBirthBS { get; set; }
        public int Age { get; set; }
        public int? ImageId { get; set; }
        public string Image { get; set; }
        /// <summary>
        /// parent detail.
        /// </summary>
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherOrganization { get; set; }
        public string FatherEmail { get; set; }
        public string FatherPhone { get; set; }
        /// <summary>
        /// mother detail.
        /// </summary>
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherOrganization { get; set; }
        public string MotherEmail { get; set; }
        public string MotherPhone { get; set; }
        public string Remark { get; set; }
        public AdmissionTypeEnum AdmissionTypeId { get; set; }
        public string AdmissionType { get; set; }
        public KnownThroughEnum KnownThroughId { get; set; }
        public string KnownThrough { get; set; }

    }
}
