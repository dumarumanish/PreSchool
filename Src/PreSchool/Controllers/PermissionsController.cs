
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.AppConfigurations.Models.Dtos;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {

        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        /// <summary>
        /// Get all the permissions of application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // [EnableQuery]
        [AuthorizeUser(Permissions.RoleManagement)]
        public async Task<List<PermissionDto>> GetAllPermissions()
        {
            return await _permissionService.GetAllPermissions();
        }

        /// <summary>
        /// Get all the permissions of application by grouping
        /// </summary>
        /// <returns></returns>
        [HttpGet("PermissionByGrouping")]
        // [EnableQuery]
        [AuthorizeUser(Permissions.RoleManagement)]
        public async Task<List<PermissionByGroupDto>> GetAllPermissionsByGrouping()
        {
            return await _permissionService.GetAllPermissionsByGrouping();
        }

        /// <summary>
        /// Get all the permissions of application as enum
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsEnum")]
        // [EnableQuery]
        [AuthorizeUser(Permissions.RoleManagement)]
        public async Task<List<string>> GetAllPermissionsAsEnum()
        {
            return await _permissionService.GetPermissionAsEnum();
        }
    }
}
