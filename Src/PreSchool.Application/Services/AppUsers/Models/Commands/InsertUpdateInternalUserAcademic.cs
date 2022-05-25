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
    public class InsertUpdateInternalUserAcademic
    {
        public int AppUserId { get; set; }
        public int Id { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specialization { get; set; }
        public string Level { get; set; }
        public string PassedYear { get; set; }
        public string Division { get; set; }
        public string Action { get; set; }


    }
}
