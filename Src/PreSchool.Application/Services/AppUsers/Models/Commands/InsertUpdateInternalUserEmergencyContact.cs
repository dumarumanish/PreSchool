using PreSchool.Data.Entities.Common.Addresses;
using PreSchool.Data.Entities.Departments;
using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Schools;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class InsertUpdateInternalUserEmergencyContact
    {
        public int AppUserId { get; set; }
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string MiddleName   { get; set; }
        public string LastName   { get; set; }
        public string Relation { get; set; }
        public string ContactNumber { get; set; }
      


    }
}
