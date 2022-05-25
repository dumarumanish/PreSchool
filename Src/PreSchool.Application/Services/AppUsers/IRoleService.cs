using PreSchool.Application.Services.AppConfigurations.Models.Commands;
using PreSchool.Application.Services.AppConfigurations.Models.Dtos;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.AppUsers
{
    public interface IRoleService
    {
        Task<bool> AddUpdatePermissionsForRole(List<AddUpdateRolePermission> addUpdateRolePermission);
        Task<bool> DeleteRole(int roleId);
        Task<List<RoleDto>> GetAllRoles();
        Task<List<PermissionDto>> GetPermissionsOfRole(int roleId);
        Task<int> InsertUpdateInternalAppUserRole(InsertUpdateRole command);
        Task<int> InsertUpdateRole(InsertUpdateRole roleCommand);
        Task<List<RoleDto>> InternalUserRoles();
        Task<RoleDetailDto> RoleById(int id);
    }
}