using PreSchool.Data.Entities.AppUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Departments
{
    public class Department : CommonProperties
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }

        public virtual ICollection<DepartmentService> DepartmentServices { get; set; }


        public Department()
        {
            AppUsers = new HashSet<AppUser>();
            DepartmentServices = new HashSet<DepartmentService>();

        }
    }
}
