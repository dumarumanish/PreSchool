using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.AppUsers;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Data.Enumerations;
using PreSchool.Application.Services.AppConfigurations.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeUser]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeUser (Permissions.RoleManagement)]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _roleService.GetAllRoles());
        }

        /// <summary>
        /// Create new role, Returns id of the new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeUser(Permissions.RoleManagement)]
        public async Task<IActionResult> InsertRole([FromBody]InsertUpdateRole role)
        {
            return Ok(await _roleService.InsertUpdateRole(role));
        }

        /// <summary>
        /// Update  role, Can be used for all type of role
        /// </summary>
        /// <param name="id">Id of the role</param>
        /// <param name="role">Role</param>
        /// <returns></returns>
        /// 
        [HttpPut("{id}")]
        [AuthorizeUser (Permissions.RoleManagement)]
        public async Task<IActionResult> UpdateRole(int id, [FromBody]InsertUpdateRole role)
        {
            if (id == 0)
                throw new BadRequestException("Id is null");
            if (id != role.Id)
                throw new BadRequestException("Id doesnot match.");
            return Ok(await _roleService.InsertUpdateRole(role));
        }


        /// <summary>
        /// Get internal user role
        /// </summary>
        /// <returns></returns>
        [HttpGet("Internal")]
        public async Task<List<RoleDto>> InternalUserRoles()
        {
            return await _roleService.InternalUserRoles();
        }


        /// <summary>
        /// Add internal user role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("Internal")]
        public async Task<int> InsertInternalUserRoles(InsertUpdateRole role)
        {
            role.Id = 0;
            return await _roleService.InsertUpdateInternalAppUserRole(role);
        }
        [HttpPut("{id}/Internal")]
        [AuthorizeUser]
        public async Task<int> UpdateInternalRole(int id, InsertUpdateRole role)
        {
            if (id == 0)
                throw new BadRequestException("Id is null");
            if (id != role.Id)
                throw new BadRequestException("Id doesnot match.");
            return await _roleService.InsertUpdateInternalAppUserRole(role);
        }

     
        /// <summary>
        /// Get role by Id, can be used for all type of roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RoleDetailDto> Role(int id)
        {

            return await _roleService.RoleById(id);
        }

        /// <summary>
        /// Add / Update permission of the role, Returns true or false
        /// </summary>
        /// <param name="rolePermissions"></param>
        /// <returns></returns>
        [HttpPost("PermissionsForRole")]
        [AuthorizeUser (Permissions.RoleManagement)]
        public async Task<IActionResult> AddUpdatePermissionsForRole([FromBody]List<AddUpdateRolePermission> rolePermissions)
        {
            return Ok(await _roleService.AddUpdatePermissionsForRole(rolePermissions));
        }


        /// <summary>
        /// Get permissions of the role
        /// </summary>
        /// <param name="id">Id of role</param>
        /// <returns></returns>
        [HttpGet("PermissionsOfRole/{id}")]
        [AuthorizeUser (Permissions.RoleManagement)]
        public async Task<IActionResult> GetPermissionsOfRole(int id)
        {
            return Ok(await _roleService.GetPermissionsOfRole(id));
        }

        /// <summary>
        /// Delete the role, Returns ture or false
        /// </summary>
        /// <param name="roleId">role Id </param>
        /// <returns></returns>
        [HttpDelete("{roleId}")]
        [AuthorizeUser(Permissions.RoleManagement)]
        public async Task<IActionResult> DeleteAppUser(int roleId)
        {
            return Ok(await _roleService.DeleteRole(roleId));
        }
    }
}