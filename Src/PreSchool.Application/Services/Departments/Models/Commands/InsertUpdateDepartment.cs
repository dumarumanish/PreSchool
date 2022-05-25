using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Departments.Models.Commands
{
   public class InsertUpdateDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

    }
}
