using PreSchool.Data.Entities.Common.Addresses;
using PreSchool.Data.Entities.Departments;
using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Schools;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.AppUsers
{
    public class InternalUserDocuments : CommonProperties
    {
        public int AppUserId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentIssuedFrom { get; set; }
        public string DocumentIssuedOn { get; set; }
        public int ImageId { get; set; }

        public virtual File Image { get; set; }
        public virtual AppUser AppUser { get; set; }


    }
}
