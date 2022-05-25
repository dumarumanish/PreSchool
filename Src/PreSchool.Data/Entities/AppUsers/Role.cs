using PreSchool.Data.Entities.Stores;
using System.Collections.Generic;

namespace PreSchool.Data.Entities.AppUsers
{
    /// <summary>
    /// Role for the application
    /// </summary>
    public class Role :CommonProperties,IStoreMappingSupported
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// For each department it is limited to particular app user type
        /// </summary>
        public int LimitedToAppUserTypeId { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public bool LimitedToStores { get ; set ; }
    
        public bool LimitedToVendors { get ; set ; }

        public int DisplayOrder { get; set; }
        public virtual AppUserType LimitedToAppUserType { get; set; }


        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
    }
}
