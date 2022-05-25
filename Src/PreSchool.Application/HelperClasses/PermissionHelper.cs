using PreSchool.Data.Attributes;
using PreSchool.Data.Entities.AppConfigurations;
using PreSchool.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace PreSchool.Application.HelperClasses
{
    public static class PermissionHelper
    {
        public static List<PermissionGroup> GetPermissionGroupsFromEnum()
        {
            var totalPermissionGroupInEnum = new List<PermissionGroup>();
            foreach (var permissionName in Enum.GetNames(typeof(PermissionGroups)))
            {
                var member = typeof(PermissionGroups).GetMember(permissionName);
                //If there is no DisplayAttribute then the Enum is not used
                var displayAttribute = member[0].GetCustomAttribute<DisplayAttribute>();


                if (displayAttribute == null)
                    continue;

                var permissionGroup = (PermissionGroups)Enum.Parse(typeof(PermissionGroups), permissionName, false);
                totalPermissionGroupInEnum.Add(
                    new PermissionGroup
                    {
                        Id = (int)permissionGroup,
                        GroupName = permissionGroup.ToString(),
                        Description = displayAttribute.Description,

                    });
            }
            return totalPermissionGroupInEnum;
        }
        public static List<Permission> GetPermissionsFromEnum()
        {
            var totalPermissionInEnum = new List<Permission>();
            foreach (var permissionName in Enum.GetNames(typeof(Permissions)))
            {
                var member = typeof(Permissions).GetMember(permissionName);
                //If there is no DisplayAttribute then the Enum is not used
                var displayAttribute = member[0].GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute == null)
                    continue;
                var customDisplayAtribute = member[0].GetCustomAttribute<PermissionLimitedToAttribute>();
                var permission = EnumHelper.ParseEnum<Permissions>(permissionName);
                totalPermissionInEnum.Add(
                    new Permission
                    {
                        Id = (int)permission,
                        Name = permissionName,
                        Description = displayAttribute.Description,
                        PermissionGroupId = (int)EnumHelper.ParseEnum<PermissionGroups>(displayAttribute.GroupName),
                        LimitedToAppUserType = customDisplayAtribute == null ? null : string.Join(",", customDisplayAtribute.LimitedToAppUserType.ToList().ConvertAll(e => (int)e).ToList()),
                        LimitedToFeatureId = (customDisplayAtribute == null || customDisplayAtribute.LimitedToFeature == null)?(int?)null :(int)customDisplayAtribute.LimitedToFeature
                    });
            }
            return totalPermissionInEnum;
        }
    }
}
