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
    public class InsertUpdateInternalUserExperience
    {
        public int AppUserId { get; set; }
        public int Id { get; set; }
        public string InstitutionName { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public string JoinDate { get; set; }
        public string LeftDate { get; set; }
        public string ReasonforLeaving { get; set; }


    }
}
