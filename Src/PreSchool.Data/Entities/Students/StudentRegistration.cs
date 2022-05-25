using PreSchool.Data.Entities.Common.Addresses;
using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Schools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public class StudentRegistration : CommonProperties
    {
        public int? StudentEnquiryId { get; set; }
        public virtual StudentEnquiry StudentEnquiry { get; set; }
        public int ImageId { get; set; }
        public string FirstName  { get; set; }
        public string MiddleName   { get; set; }
        public string LastName   { get; set; }
        public GenderEnum GenderId { get; set; }
        public DateTime DateOfBirthAD { get; set; }
        public string DateOfBirthBS { get; set; }
        public string BloodGroup { get; set; }
        /// <summary>
        /// for nepalease citizen
        /// </summary>
        public string BirthCertificateNo { get; set; }
        public int? IssueCityId { get; set; }
        public virtual City IssueCity { get; set; }

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
        public virtual Batch Batch { get; set; }
        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }
        public string Section { get; set; }
        public string RegistrationNo { get; set; }
        public int RollNo { get; set; }
        public StudentTypeEnum StudentTypeId { get; set; }

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

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual File Image { get; set; }


    }
}
