using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsersDepartments.Models.Dtos
{
    public class DepartmentServiceDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
