using PreSchool.Application.Services.AppUsersDepartments.Models.Commands;
using PreSchool.Application.Services.AppUsersDepartments.Models.Dtos;
using PreSchool.Application.Services.Departments.Models.Commands;
using PreSchool.Application.Services.Departments.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Departments
{
    public interface IAppUserDepartmentService
    {
        Task<bool> DeleteDepartment(int Id);
        Task<bool> DeleteDepartmentService(int Id);
        Task<DepartmentServiceDto> DepartmentServiceById(int departmentId, int id);
        Task<List<DepartmentServiceDto>> DepartmentServices(int departmentId);
        Task<List<DepartmentDto>> GetAllDepartments();
        Task<DepartmentDto> GetDepartmentById(int Id);
        Task<int> InsertUpdateDepartment(InsertUpdateDepartment departmentCommand);
        Task<int> InsertUpdateDepartmentService(InsertUpdateDepartmentService command);
    }
}