using PreSchool.Application.Services.AppConfigurations.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.AppConfigurations
{
    public interface IPermissionService
    {
        Task<List<PermissionDto>> GetAllPermissions();
        Task<List<PermissionByGroupDto>> GetAllPermissionsByGrouping();
        Task<List<string>> GetPermissionAsEnum();
    }
}