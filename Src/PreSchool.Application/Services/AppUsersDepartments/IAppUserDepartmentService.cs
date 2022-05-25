using PreSchool.Application.Services.Addresses.Models.Filters;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.AppUsersDepartments
{
    public interface IAppUserDepartmentService
    {
        Task<AppUserDepartmentDetailDto> AppUserDepartmentById(int id);
        Task<List<AppUserDepartmentDto>> AppUserDepartments(DepartmentFilter filter);
        Task<int> InsertUpdateAppUserDepartment(InsertUpdateDepartment command);
        Task<int> InsertUpdateInternalAppUserDepartment(InsertUpdateDepartment command);
        Task<int> InsertUpdateStoreAppUserDepartment(InsertUpdateStoreDepartment command);
        Task<int> InsertUpdateVendorAppUserDepartment(InsertUpdateVendorDepartment command);
        Task<List<AppUserDepartmentDto>> InternalUserDepartments();
        Task<List<AppUserDepartmentDto>> StoreDepartments(int storeId);
        Task<List<AppUserDepartmentDto>> VendorDepartments(int storeId, int vendorId);
    }
}