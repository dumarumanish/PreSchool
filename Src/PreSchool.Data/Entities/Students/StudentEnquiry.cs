using PreSchool.Data.Entities.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public class StudentEnquiry : CommonProperties
    {
 
        public string FirstName  { get; set; }
        public string MiddleName   { get; set; }
        public string LastName   { get; set; }
        public GenderEnum GenderId { get; set; }
        public DateTime DateOfBirthAD { get; set; }
        public string DateOfBirthBS { get; set; }
        public int Age { get; set; }
        public int? ImageId { get; set; }
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
        public int? AddressId { get; set; }
        public AdmissionTypeEnum AdmissionTypeId { get; set; }
        public KnownThroughEnum KnownThroughId { get; set; }
        public virtual Address Address { get; set; }
        public virtual File Image { get; set; }
        public virtual ICollection<StudentRegistration> StudentRegistrations { get; set; }



        public StudentEnquiry()
        {

            StudentRegistrations = new HashSet<StudentRegistration>();
        
        }
 
    }
}
