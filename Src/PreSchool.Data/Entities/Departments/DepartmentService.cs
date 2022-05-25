using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Departments
{
    public class DepartmentService : CommonProperties
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}
