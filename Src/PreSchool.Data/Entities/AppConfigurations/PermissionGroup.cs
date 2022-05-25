using System.Collections.Generic;

namespace PreSchool.Data.Entities.AppConfigurations
{
    public class PermissionGroup 
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }

    }
}
