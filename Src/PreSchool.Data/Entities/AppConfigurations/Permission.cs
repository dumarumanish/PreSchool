using PreSchool.Data.Entities.AppUsers;
using System.Collections.Generic;

namespace PreSchool.Data.Entities.AppConfigurations
{
    /// <summary>
    /// Permission of the application
    /// </summary>
    public class Permission 
    {
        // Permission Id
        public int Id { get; set; }

        public int PermissionGroupId { get; set; }

        // Name of the Permission 
        public string Name { get; set; }

        // Description of the permission  discription
        public string Description { get; set; }

        /// <summary>
        /// Comma seperated appuser type 
        /// if null, permission is visible to all the users,
        /// else to the selected appuser type
        /// </summary>
        public string LimitedToAppUserType { get; set; }

        /// <summary>
        /// Comma seperated feature
        /// if null, permission is visible to all the users,
        /// else to the selected feature
        /// </summary>
        public int? LimitedToFeatureId{ get; set; }


        public virtual PermissionGroup PermissionGroup { get; set; }

        public virtual AppFeature LimitedToFeature { get; set; }


        public virtual ICollection<RolePermission> RolePermissions { get; set; }

    }
}
