using System.Collections.Generic;

namespace PreSchool.Application.Services.AppConfigurations.Models.Dtos
{
    public class PermissionByGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}
