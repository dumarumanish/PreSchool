using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using PreSchool.Data;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Enumerations;
using PreSchool.Application.Services.AppConfigurations.Models.Dtos;
using PreSchool.Application.Services.AppConfigurations.Models.Commands;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.AppConfigurations;

namespace PreSchool.Application.Services.AppUsers
{

    public class RoleService : IRoleService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppFeatureService _appFeatureService;
        private readonly IDateTime _dateTime;

        public RoleService(
            IApplicationDbContext context,
            ICurrentUserService currentUserService,
            IAppFeatureService appFeatureService,
            IDateTime dateTime
            )
        {
            _context = context;
            _currentUserService = currentUserService;
            _appFeatureService = appFeatureService;
            _dateTime = dateTime;
        }


        public async Task<List<RoleDto>> GetAllRoles()
        {
            // Return all role except superadmin.
            var roles = _context.Roles
                        .AsNoTracking()
                        .Where(r => !r.IsSystemRole);

            if (_currentUserService.AppUserType != AppUserTypeEnum.Internal)
            {
                roles = roles.Where(r => r.LimitedToAppUserTypeId == (int)_currentUserService.AppUserType);
            }

            return await roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            })
                .ToListAsync();
        }

        public async Task<int> InsertUpdateRole(InsertUpdateRole roleCommand)
        {
            // Check if the role is reserved 
            var roleName = roleCommand.Name.ToLower().Trim();
            var reservedRole = Enum.GetValues(typeof(ReservedRoles)).Cast<ReservedRoles>();
            if (reservedRole.Select(v => v.ToString().ToLower()).Contains(roleName))
                throw new BadRequestException("Reserved role", "Cannot add/update the reserved role");

            if (roleCommand.Id == 0)
            {
                var role = new Role
                {
                    Id = roleCommand.Id,
                    Description = roleCommand.Description,
                    Name = roleCommand.Name,



                };
                role.ModifiedBy = null;
                role.ModifiedOn = null;
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();

                // Store mapping
                if (_currentUserService.StoreId != null)
                    _context.StoreMappings.Add(new Data.Entities.Stores.StoreMapping
                    {
                        EntityId = role.Id,
                        EntityName = nameof(Role),
                        StoreId = _currentUserService.StoreId ?? 0
                    });


                await _context.SaveChangesAsync();
                return role.Id;
            }

            // Update
            var existingRole = _context.Roles.FirstOrDefault(r => r.Id == roleCommand.Id);
            if (existingRole == null)
                throw new NotFoundException("Invalid role", "Role not found, Check roleId");

            var existingRoleName = existingRole.Name;
            // check if the role is reserved

            if (reservedRole.Select(v => (int)v).Contains(existingRole.Id))
                throw new BadRequestException("Reserved role", "Cannot add/update the reserved role");

            existingRole.Description = roleCommand.Description;
            existingRole.Name = roleCommand.Name;
            await _context.SaveChangesAsync();
            return roleCommand.Id;

        }

        #region Internal user role

        public async Task<List<RoleDto>> InternalUserRoles()
        {
            if (_currentUserService.AppUserType != AppUserTypeEnum.Internal)
                throw new ForbiddenException();

            if (!_currentUserService.HavePermission(Permissions.RoleManagement))
                throw new ForbiddenException();

            return await _context.Roles
                                .AsNoTracking()
                                .Where(d => d.LimitedToAppUserTypeId == (int)_currentUserService.AppUserType && d.Name != "SuperAdmin")
                                .OrderBy(d => d.DisplayOrder)
                                .Select(d => new RoleDto
                                {
                                    Description = d.Description,
                                    DisplayOrder = d.DisplayOrder,
                                    Id = d.Id,
                                    Name = d.Name,

                                })
                                .ToListAsync();
        }

        public async Task<int> InsertUpdateInternalAppUserRole(InsertUpdateRole command)
        {
            //// Check for appUser type

            if (_currentUserService.AppUserType != AppUserTypeEnum.Internal)
                throw new ForbiddenException();

            if (!_currentUserService.HavePermission(Permissions.RoleManagement))
                throw new ForbiddenException();

            // New role
            if (command.Id == 0)
            {
                var role = new Role
                {
                    Description = command.Description,
                    LimitedToAppUserTypeId = (int)_currentUserService.AppUserType,
                    LimitedToStores = false,
                    LimitedToVendors = false,
                    Name = command.Name,
                    DisplayOrder = command.DisplayOrder,

                };
                _context.Roles.Add(role);

                await _context.SaveChangesAsync();
                return role.Id;
            }


            // Get role
            var existingRole = await _context.Roles
                                .FirstOrDefaultAsync(d => d.Id == command.Id);
            if (existingRole == null)
                throw new NotFoundException("Invalid role", "Role not found");
            existingRole.Description = command.Description;
            existingRole.Name = command.Name;
            existingRole.DisplayOrder = command.DisplayOrder;

            await _context.SaveChangesAsync();
            return existingRole.Id;

        }

        #endregion

        public async Task<RoleDetailDto> RoleById(int id)
        {
            var role = await _context.Roles
                              .AsNoTracking()
                              .FirstOrDefaultAsync(d => d.Id == id);

            if (role == null)
                throw new NotFoundException("Role not found");

            // Check for limited to store
            var limitedToStore = new LimitedToStoresDto
            {
                LimitedToStores = role.LimitedToStores,
            };
            if (role.LimitedToStores)
            {
                limitedToStore.Stores = await _context.StoreMappings
                                .Where(s => s.EntityId == role.Id && s.EntityName == nameof(Role))
                                .Select(s => s.StoreId)
                                .ToListAsync();
            }
            return new RoleDetailDto
            {
                Description = role.Description,
                DisplayOrder = role.DisplayOrder,
                Id = role.Id,
                Name = role.Name,
                LimitedToStores = limitedToStore,
            };
        }

        public async Task<bool> AddUpdatePermissionsForRole(List<AddUpdateRolePermission> addUpdateRolePermission)
        {
            if (addUpdateRolePermission == null || addUpdateRolePermission.Count == 0)
                throw new BadRequestException("No role and permisson are selected");



            var rolePermission = addUpdateRolePermission.Select(r => new RolePermission
            {
                PermissionId = r.PermissionId,
                RoleId = r.RoleId,
                IsDeleted = r.IsDeleted
            });
            foreach (var permission in rolePermission)
            {
                if (permission.RoleId == 0 || permission.PermissionId == 0)
                    throw new BadRequestException("Role or permission is not selected", $"RoleId :{permission.RoleId} and PermissionId: {permission.PermissionId}");

                // Check if the permission is total Access
                if (permission.PermissionId == (int)Data.Enumerations.Permissions.TotalAccess)
                    throw new BadRequestException("Invalid permission", "Invalid Permission Id");

                // Check if there is role 
                if (await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == permission.RoleId) == null)
                    throw new BadRequestException("Invalid Role", "Role doesnot exists.");

                // Check if there is permission
                if (await _context.Permissions.AsNoTracking().FirstOrDefaultAsync(p => p.Id == permission.PermissionId) == null)
                    throw new BadRequestException("Invalid permission", "Permission doesnot exists.");

                var existingRP = _context.RolePermissions
                    .IgnoreQueryFilters()
                    .FirstOrDefault(rp => rp.RoleId == permission.RoleId && rp.PermissionId == permission.PermissionId);

                // New permission
                if (existingRP == null)
                {

                    _context.RolePermissions.Add(permission);


                }
                // Existing permission
                else
                {

                    existingRP.IsDeleted = permission.IsDeleted;
                }
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<PermissionDto>> GetPermissionsOfRole(int roleId)
        {
            // Check if role is superadmin
            if (roleId == (int)ReservedRoles.SuperAdmin)
                throw new BadRequestException("Invalid role", "Role doesnot exists");
            return (await _context.Roles
                .Include(r => r.RolePermissions)
                    .ThenInclude(r => r.Permission)
                        .ThenInclude(p => p.PermissionGroup)
                .FirstOrDefaultAsync(r => r.Id == roleId))
                ?.RolePermissions
                .Select(rp => new PermissionDto
                {
                    Id = rp.PermissionId,
                    PermissionName = rp.Permission.Name,
                    Description = rp.Permission.Description,
                    GroupName = rp.Permission.PermissionGroup.GroupName,
                    PermissionGroupId = rp.Permission.PermissionGroupId
                }).ToList();
        }

        public async Task<bool> DeleteRole(int roleId)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
                throw new NotFoundException("Invalid role", "Role not found, Check roleId");


            var existingRoleName = role.Name.ToLower();
            // check if the role is reserved
            var reservedRole = Enum.GetValues(typeof(ReservedRoles)).Cast<ReservedRoles>();
            if (reservedRole.Select(v => (int)v).Contains(roleId))
                throw new BadRequestException("Reserved role", "Cannot delete the reserved role");


            var isRoleUsed = await _context.AppUserRoles.AsNoTracking().FirstOrDefaultAsync(r => r.RoleId == roleId) != null;

            if (isRoleUsed)
                throw new NotAllowedException("Role cannot be deleted. It is being used by other record.");


            role.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
