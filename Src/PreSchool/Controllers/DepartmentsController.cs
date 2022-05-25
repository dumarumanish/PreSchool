using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.AppUsersDepartments.Models.Commands;
using PreSchool.Application.Services.AppUsersDepartments.Models.Dtos;
using PreSchool.Application.Services.Departments;
using PreSchool.Application.Services.Departments.Models.Commands;
using PreSchool.Application.Services.Departments.Models.Dtos;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IAppUserDepartmentService _departmentService;

        public DepartmentsController(IAppUserDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        #region Department.

        [HttpGet]
       // [AuthorizeUser(Permissions.ReadDepartment)]

        public async Task<List<DepartmentDto>> GetAllDepartments()
        {
            return await _departmentService.GetAllDepartments();

        }

        [HttpGet("{Id}")]
       // [AuthorizeUser(Permissions.ReadDepartment)]

        public async Task<DepartmentDto> GetDepartmentById(int Id)
        {
            return await _departmentService.GetDepartmentById(Id);

        }

        //Insert Department.

        [HttpPost]
        //[AuthorizeUser(Permissions.CreateDepartment)]

        public async Task<int> InsertDepartment(InsertUpdateDepartment departmentCommand)
        {
            departmentCommand.Id = 0;
            return await _departmentService.InsertUpdateDepartment(departmentCommand);

        }

        [HttpPut("{id}")]
       // [AuthorizeUser(Permissions.UpdateDepartment)]

        public async Task<int> UpdateDepartment(int id, InsertUpdateDepartment departmentCommand)
        {
            if (id != departmentCommand.Id)
                throw new BadRequestException("Invalid Id", "Id doesnot match");
            if (id == 0)
                throw new BadRequestException("Invalid id","id cannot be 0");

            return await _departmentService.InsertUpdateDepartment(departmentCommand);

        }
        [HttpDelete("{Id}")]
       // [AuthorizeUser(Permissions.DeleteDepartment)]

        public async Task<bool> DeleteDepartment(int Id)
        {

            return await _departmentService.DeleteDepartment(Id);

        }

        #endregion

        #region DepartmentService service

        [HttpGet("{departmentId}/Services")]
        public async Task<List<DepartmentServiceDto>> DepartmentServices(int departmentId)
        {
            return await _departmentService.DepartmentServices(departmentId);

        }

        [HttpPost("{departmentId}/Services")]
        public async Task<int> InsertUpdateDepartmentService(int departmentId, InsertUpdateDepartmentService command)
        {
            if (departmentId == 0)
                throw new BadRequestException("Department Id is null");

            if (departmentId != command.DepartmentId)
                throw new BadRequestException("Department Id doesnot match.");

            command.Id = 0;

            return await _departmentService.InsertUpdateDepartmentService(command);


        }

        [HttpPut("{departmentId}/Services/{id}")]
        public async Task<int> InsertUpdateDepartmentService(int departmentId, int id, InsertUpdateDepartmentService command)
        {

            if (departmentId == 0)
                throw new BadRequestException("Department Id is null");

            if (departmentId != command.DepartmentId)
                throw new BadRequestException("Department Id doesnot match.");

            if (id == 0)
                throw new BadRequestException("Id cannot be 0");

            if (id != command.Id)
                throw new BadRequestException("Id doesnot match.");

            return await _departmentService.InsertUpdateDepartmentService(command);

        }


        [HttpGet("{departmentId}/Services/{id}")]
        public async Task<DepartmentServiceDto> DepartmentServiceById(int departmentId, int id)
        {
            return await _departmentService.DepartmentServiceById(departmentId, id);

        }

        [HttpDelete("Services/{Id}")]
      //  [AuthorizeUser(Permissions.DeleteDepartment)]

        public async Task<bool> DeleteDepartmentService(int Id)
        {

            return await _departmentService.DeleteDepartmentService(Id);

        }

        #endregion
    }
}
