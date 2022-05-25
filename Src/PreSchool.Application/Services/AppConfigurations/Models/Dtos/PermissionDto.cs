using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppConfigurations.Models.Dtos
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public int PermissionGroupId { get; set; }
        public string PermissionName { get; set; }
        public string GroupName { get; set; }
        public string  Description { get; set; }
    }
}
