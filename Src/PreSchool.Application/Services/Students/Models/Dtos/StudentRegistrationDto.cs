using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Dtos
{
    public class StudentRegistrationDto
    {
        public int Id { get; set; }
        public int? StudentEnquiryId { get; set; }
        public int ImageId { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public GenderEnum GenderId { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirthAD { get; set; }
        public string DateOfBirthBS { get; set; }
        public string BloodGroup { get; set; }
        /// <summary>
        /// for nepalease citizen
        /// </summary>
        public string BirthCertificateNo { get; set; }
        public int? IssueCityId { get; set; }
        public string CityName { get; set; }

        public string PlaceOfIssue { get; set; }
        public DateTime? DateOfIssueAD { get; set; }
        public string DateOfIssueBS { get; set; }
        /// <summary>
        /// For SAARC & Other Students
        /// </summary>
        public string PassportNo { get; set; }
        public DateTime? ValidUpToAD { get; set; }
        public string VisaCategory { get; set; }
        /// <summary>
        /// Admission Details
        /// </summary>
        public DateTime DateOfEnrollmentAD { get; set; }
        public int BatchId { get; set; }
        public string Batch { get; set; }
        public int GradeId { get; set; }
        public string Grade { get; set; }
        public string Section { get; set; }
        public string RegistrationNo { get; set; }
        public int RollNo { get; set; }
        public StudentTypeEnum StudentTypeId { get; set; }
        public string StudentType { get; set; }

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
        /// <summary>
        /// Guardian detail.
        /// </summary>
        public string GuardianName { get; set; }
        public string RelationWithGuardian { get; set; }
        public string GuardianAddress { get; set; }
        public string GuardianEmail { get; set; }
        public string GuardianPhone { get; set; }

        public AddressDto Address { get; set; }

    }
}
