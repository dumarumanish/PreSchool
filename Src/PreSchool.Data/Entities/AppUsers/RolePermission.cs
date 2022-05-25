using PreSchool.Data.Entities.AppConfigurations;

namespace PreSchool.Data.Entities.AppUsers
{

    /// <summary>
    /// Permission for a particular role
    /// </summary>
    public class RolePermission :CommonProperties
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }

    }
}
