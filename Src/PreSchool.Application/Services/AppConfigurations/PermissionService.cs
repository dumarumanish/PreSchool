using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.AppConfigurations.Models.Dtos;

namespace PreSchool.Application.Services.AppConfigurations
{
    public class PermissionService : IPermissionService
    {
        private readonly IApplicationDbContext _context;

        public PermissionService(
            IApplicationDbContext context
            )
        {
            _context = context;

        }
        public Task<List<PermissionDto>> GetAllPermissions()
        {
            // Return permission list without Total Access
            return _context.Permissions
                .Select(p => new PermissionDto
                {
                    Id = p.Id,
                    PermissionGroupId = p.PermissionGroupId,
                    GroupName = p.PermissionGroup.GroupName,
                    PermissionName = p.Name,
                    Description = p.Description,
                })
                .Where(p => p.PermissionName != "TotalAccess")
                .ToListAsync();
        }

        public async Task<List<PermissionByGroupDto>> GetAllPermissionsByGrouping()
        {
            // Return permission list without Total Access
            var permissionsGroups = await _context.PermissionGroups
                .Include(p => p.Permissions)
                .ToListAsync();

            List<PermissionByGroupDto> pgs = new List<PermissionByGroupDto>();

            foreach (var pg in permissionsGroups)
            {
                if (pg.GroupName == "SuperAdmin")
                    continue;
                var pgDto = new PermissionByGroupDto()
                {
                    GroupId = pg.Id,
                    GroupName = pg.GroupName,
                    Permissions = pg.Permissions.Select(p => new PermissionDto
                    {
                        Id = p.Id,
                        PermissionGroupId = p.PermissionGroupId,
                        GroupName = p.PermissionGroup.GroupName,
                        PermissionName = p.Name,
                        Description = p.Description,
                    })
                .ToList()
                };

                pgs.Add(pgDto);
            }
            return pgs;
        }

        public async Task<List<string>> GetPermissionAsEnum()
        {
            var permissions = await _context.Permissions
                                .AsNoTracking()
                                .ToListAsync();

            var permissionAsEnumLists = new List<string>();
            foreach (var permission in permissions)
            {
                permissionAsEnumLists.Add(permission.Name + " = " + permission.Id);
            }
            return permissionAsEnumLists;
        }
    }


}
