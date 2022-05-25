using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.AppUsers;
using PreSchool.Application.Services.Departments.Models.Commands;
using PreSchool.Application.Services.Departments.Models.Dtos;
using PreSchool.Application.Services.AppUsersDepartments.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreSchool.Application.Services.AppUsersDepartments.Models.Commands;
using PreSchool.Data.Enumerations;
using PreSchool.Data.Entities.Departments;

namespace PreSchool.Application.Services.Departments
{
    public class AppUserDepartmentService : IAppUserDepartmentService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;
        private readonly ICurrentUserService _currentUserService;
        private readonly AppSettings _appSettings;

        public AppUserDepartmentService(
           IApplicationDbContext context,
            IDateTime dateTime,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor,
            IJwtService jwtService,
            ICurrentUserService currentUserService
            )
        {
            _context = context;
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
            _currentUserService = currentUserService;
            _appSettings = appSettings.Value;


        }

        #region Department.

        public Task<List<DepartmentDto>> GetAllDepartments()
        {
            return _context.Departments
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    SortOrder = d.SortOrder,
                    IsActive = d.IsActive,
                })
                .ToListAsync();
        }
        public async Task<DepartmentDto> GetDepartmentById(int Id)
        {
            var department = await _context.Departments
                .OrderBy(i => i.SortOrder)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(f => f.Id == Id);
            if (department == null)
                throw new NotFoundException("department not found");

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                SortOrder = department.SortOrder,
                IsActive = department.IsActive,
            };
        }

        //Insert department.
        public async Task<int> InsertUpdateDepartment(InsertUpdateDepartment departmentCommand)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(u => u.Name == departmentCommand.Name);

            if (departmentCommand.Id == 0)
            {
                if (department != null)
                    throw new BadRequestException("Invalid name", "Name is already taken");

                var departmentType = new Data.Entities.Departments.Department
                {
                    Name = departmentCommand.Name,
                    Description = departmentCommand.Description,
                    SortOrder = departmentCommand.SortOrder,
                    IsActive = departmentCommand.IsActive,
                };
                _context.Departments.Add(departmentType);
                await _context.SaveChangesAsync();
                return departmentType.Id;
            }

            // Update

            var existingDepartment = _context.Departments.FirstOrDefault(c => c.Id == departmentCommand.Id);
            if (existingDepartment == null)
                throw new NotFoundException("Invalid department", "department not found, Check department Id");

            existingDepartment.Name = departmentCommand.Name;
            existingDepartment.Description = departmentCommand.Description;
            existingDepartment.SortOrder = departmentCommand.SortOrder;
            existingDepartment.IsActive = departmentCommand.IsActive;


            await _context.SaveChangesAsync();
            return departmentCommand.Id;

        }

        //Delete.
        public async Task<bool> DeleteDepartment(int Id)
        {

            var department = await _context.Departments
                .FirstOrDefaultAsync(f => f.Id == Id);

            if (department == null)
                throw new NotFoundException("Invalid department", "department not found, Check department Id");

            var departmentServices = await _context.DepartmentServices
                          .FirstOrDefaultAsync(f => f.DepartmentId == Id);
            if (departmentServices != null)
                throw new NotFoundException("Invalid", "department is used in the department service.");

            department.IsDeleted = true;

            return (await _context.SaveChangesAsync()) > 0;
        }

        #endregion

        #region DepartmentService service


        public async Task<List<DepartmentServiceDto>> DepartmentServices(int departmentId)
        {

            return await _context.DepartmentServices
                .Where(d => d.DepartmentId == departmentId)
                .OrderBy(d => d.DisplayOrder)
                                .Select(d => new DepartmentServiceDto
                                {
                                    Description = d.Description,
                                    DisplayOrder = d.DisplayOrder,
                                    Id = d.Id,
                                    Name = d.Name,
                                    DepartmentId = d.DepartmentId
                                })
                                .ToListAsync();
        }

        public async Task<int> InsertUpdateDepartmentService(InsertUpdateDepartmentService command)
        {

            var department = await _context.Departments
                .Include(a => a.DepartmentServices)
                .FirstOrDefaultAsync(d => d.Id == command.DepartmentId);

            if (department == null)
                throw new BadRequestException("Invalid department");


            //if (!_currentUserService.HavePermission(Permissions.DepartmentManagement))
            //    throw new ForbiddenException();

            if (command.Id == 0)
            {
                var departmentService = new DepartmentService
                {
                    Description = command.Description,
                    Name = command.Name,
                    DisplayOrder = command.DisplayOrder,

                };
                department.DepartmentServices.Add(departmentService);
                await _context.SaveChangesAsync();
                return departmentService.Id;

            }
            else
            {
                // Get departmentService
                var departmentService = department.DepartmentServices
                                   .FirstOrDefault(d => d.Id == command.Id);
                if (departmentService == null)
                    throw new NotFoundException("Invalid department service", "Department Service not found");
                departmentService.Description = command.Description;
                departmentService.Name = command.Name;
                departmentService.DisplayOrder = command.DisplayOrder;

                await _context.SaveChangesAsync();
                return departmentService.Id;
            }


        }



        public async Task<DepartmentServiceDto> DepartmentServiceById(int departmentId, int id)
        {
            var departmentService = await _context.DepartmentServices
                              .AsNoTracking()
                              .FirstOrDefaultAsync(d => d.Id == id && d.DepartmentId == departmentId);

            if (departmentService == null)
                throw new NotFoundException("Department Service not found");



            return new DepartmentServiceDto
            {
                Description = departmentService.Description,
                DisplayOrder = departmentService.DisplayOrder,
                Id = departmentService.Id,
                Name = departmentService.Name,
                DepartmentId = departmentService.DepartmentId
            };
        }

        //Delete.
        public async Task<bool> DeleteDepartmentService(int Id)
        {

            var departmentService = await _context.DepartmentServices
                .FirstOrDefaultAsync(f => f.Id == Id);

            if (departmentService == null)
                throw new NotFoundException("Invalid department service", "department service not found, Check department service Id");

            departmentService.IsDeleted = true;

            return (await _context.SaveChangesAsync()) > 0;
        }

        #endregion
    }
}
