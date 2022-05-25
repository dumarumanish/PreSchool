using PreSchool.Application.Exceptions;
using PreSchool.Application.HelperClasses;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.Files;
using PreSchool.Data;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Data.Entities.Schools;
using PreSchool.Application.Services.Schools.Models.Commands;
using PreSchool.Application.Services.Schools.Models.Dtos;

namespace PreSchool.Application.Services.Schools
{
    public class SchoolService : ISchoolService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;
        private readonly IAppFeatureService _appFeatureService;

        public SchoolService(
            IApplicationDbContext context,
            IDateTime dateTime,
            ICurrentUserService currentUserService,
            IFileService fileService,
            IAppFeatureService appFeatureService
            )
        {
            _context = context;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _fileService = fileService;
            _appFeatureService = appFeatureService;
        }

        #region school.

        public async Task<int> InsertUpdateSchool(InsertUpdateSchool schoolCmd)
        {
            var existing = await _context.Schools
                   .Include(s => s.Address)
                   .FirstOrDefaultAsync();

            if (existing == null)
            {
                var newSchool = new School
                {
                    AdminComment = schoolCmd.AdminComment,
                    Name = schoolCmd.Name,
                };
                // Address
                if (schoolCmd.Address != null)
                {
                    if (newSchool.Address == null)
                        newSchool.Address = new Data.Entities.Address();

                    newSchool.Address.Email = schoolCmd.Address.Email;
                    newSchool.Address.CountryId = schoolCmd.Address.CountryId == 0 ? null : schoolCmd.Address.CountryId;
                    newSchool.Address.CountryProvinceId = schoolCmd.Address.CountryProvinceId == 0 ? null : schoolCmd.Address.CountryProvinceId;
                    newSchool.Address.CityId = schoolCmd.Address.CityId;
                    newSchool.Address.RegionId = schoolCmd.Address.RegionId;
                    newSchool.Address.Address1 = schoolCmd.Address.Address1;
                    newSchool.Address.Address2 = schoolCmd.Address.Address2;
                    newSchool.Address.ZipPostalCode = schoolCmd.Address.ZipPostalCode;
                    newSchool.Address.PhoneNumber = schoolCmd.Address.PhoneNumber;
                    newSchool.Address.SecondaryPhoneNumber = schoolCmd.Address.SecondaryPhoneNumber;
                    newSchool.Address.FaxNumber = schoolCmd.Address.FaxNumber;
                    newSchool.Address.MapLink = schoolCmd.Address.MapLink; ;
                }
                _context.Schools.Add(newSchool);
                await _context.SaveChangesAsync();
                return newSchool.Id;
            }
            else
            {

                existing.AdminComment = schoolCmd.AdminComment;
                existing.Name = schoolCmd.Name;

                // Address
                if (schoolCmd.Address != null)
                {
                    if (existing.Address == null)
                        existing.Address = new Data.Entities.Address();

                    existing.Address.Email = schoolCmd.Address.Email;
                    existing.Address.CountryId = schoolCmd.Address.CountryId == 0 ? null : schoolCmd.Address.CountryId;
                    existing.Address.CountryProvinceId = schoolCmd.Address.CountryProvinceId == 0 ? null : schoolCmd.Address.CountryProvinceId;
                    existing.Address.CityId = schoolCmd.Address.CityId;
                    existing.Address.RegionId = schoolCmd.Address.RegionId;
                    existing.Address.Address1 = schoolCmd.Address.Address1;
                    existing.Address.Address2 = schoolCmd.Address.Address2;
                    existing.Address.ZipPostalCode = schoolCmd.Address.ZipPostalCode;
                    existing.Address.PhoneNumber = schoolCmd.Address.PhoneNumber;
                    existing.Address.SecondaryPhoneNumber = schoolCmd.Address.SecondaryPhoneNumber;
                    existing.Address.FaxNumber = schoolCmd.Address.FaxNumber;
                    existing.Address.MapLink = schoolCmd.Address.MapLink;
                }
                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<SchoolListDto>> GetSchoolList()
        {
            return await _context.Schools
                .AsNoTracking()
                .Select(s => new SchoolListDto
                {
                    Id = s.Id,
                    AdminComment = s.AdminComment,
                    Name = s.Name,
                }).ToListAsync();
        }

        public async Task<SchoolDto> GetSchoolById(int id)
        {
            var school = await _context.Schools
                .Include(n => n.Address)
                .ThenInclude(a => a.City)
                .Include(n => n.Address)
                .ThenInclude(a => a.CountryProvince)
                  .Include(n => n.Address)
                .ThenInclude(a => a.Country)
                   .Include(n => n.Address)
                .ThenInclude(a => a.Region)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (school == null)
                throw new NotFoundException("school not found");

            return new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                AdminComment = school.AdminComment,

                Address = school.Address == null ? null : new Addresses.Models.Dtos.AddressDto
                {
                    Id = school.Address.Id,
                    Email = school.Address.Email,
                    CountryId = school.Address.CountryId,
                    CountryProvinceId = school.Address.CountryProvinceId,
                    City = school.Address.City?.Name,
                    CityId = school.Address.CityId,
                    RegionId = school.Address.RegionId,
                    Country = school.Address.Country?.Name,
                    Region = school.Address.Region?.Name,
                    CountryProvince = school.Address.CountryProvince?.Name,
                    Address1 = school.Address.Address1,
                    Address2 = school.Address.Address2,
                    ZipPostalCode = school.Address.ZipPostalCode,
                    PhoneNumber = school.Address.PhoneNumber,
                    SecondaryPhoneNumber = school.Address.SecondaryPhoneNumber,
                    FaxNumber = school.Address.FaxNumber,
                    MapLink = school.Address.MapLink
                },

            };
        }
        #endregion

        #region branch.

        public async Task<int> InsertUpdateBranch(InsertUpdateBranch branchCmd)
        {

            if (branchCmd.Id == 0)
            {
                var newBranch = new Branch
                {
                    AdminComment = branchCmd.AdminComment,
                    Name = branchCmd.Name,
                };
                // Address
                if (branchCmd.Address != null)
                {
                    if (newBranch.Address == null)
                        newBranch.Address = new Data.Entities.Address();

                    newBranch.Address.Email = branchCmd.Address.Email;
                    newBranch.Address.CountryId = branchCmd.Address.CountryId == 0 ? null : branchCmd.Address.CountryId;
                    newBranch.Address.CountryProvinceId = branchCmd.Address.CountryProvinceId == 0 ? null : branchCmd.Address.CountryProvinceId;
                    newBranch.Address.CityId = branchCmd.Address.CityId;
                    newBranch.Address.RegionId = branchCmd.Address.RegionId;
                    newBranch.Address.Address1 = branchCmd.Address.Address1;
                    newBranch.Address.Address2 = branchCmd.Address.Address2;
                    newBranch.Address.ZipPostalCode = branchCmd.Address.ZipPostalCode;
                    newBranch.Address.PhoneNumber = branchCmd.Address.PhoneNumber;
                    newBranch.Address.SecondaryPhoneNumber = branchCmd.Address.SecondaryPhoneNumber;
                    newBranch.Address.FaxNumber = branchCmd.Address.FaxNumber;
                    newBranch.Address.MapLink = branchCmd.Address.MapLink; ;
                }
                _context.Branches.Add(newBranch);
                await _context.SaveChangesAsync();
                return newBranch.Id;
            }
            else
            {
                var existing = await _context.Branches
                                      .Include(s => s.Address)
                                      .FirstOrDefaultAsync(i => i.Id == branchCmd.Id);
                if (existing == null)
                    throw new NotFoundException("branch not found");

                existing.AdminComment = branchCmd.AdminComment;
                existing.Name = branchCmd.Name;

                // Address
                if (branchCmd.Address != null)
                {
                    if (existing.Address == null)
                        existing.Address = new Data.Entities.Address();

                    existing.Address.Email = branchCmd.Address.Email;
                    existing.Address.CountryId = branchCmd.Address.CountryId == 0 ? null : branchCmd.Address.CountryId;
                    existing.Address.CountryProvinceId = branchCmd.Address.CountryProvinceId == 0 ? null : branchCmd.Address.CountryProvinceId;
                    existing.Address.CityId = branchCmd.Address.CityId;
                    existing.Address.RegionId = branchCmd.Address.RegionId;
                    existing.Address.Address1 = branchCmd.Address.Address1;
                    existing.Address.Address2 = branchCmd.Address.Address2;
                    existing.Address.ZipPostalCode = branchCmd.Address.ZipPostalCode;
                    existing.Address.PhoneNumber = branchCmd.Address.PhoneNumber;
                    existing.Address.SecondaryPhoneNumber = branchCmd.Address.SecondaryPhoneNumber;
                    existing.Address.FaxNumber = branchCmd.Address.FaxNumber;
                    existing.Address.MapLink = branchCmd.Address.MapLink;
                }
                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<BranchListDto>> BranchList()
        {
            return await _context.Branches
                .AsNoTracking()
                .Select(s => new BranchListDto
                {
                    Id = s.Id,
                    AdminComment = s.AdminComment,
                    Name = s.Name,
                }).ToListAsync();
        }

        public async Task<BranchDto> GetBranchById(int id)
        {
            var branch = await _context.Branches
       .Include(n => n.Address)
                .ThenInclude(a => a.City)
                .Include(n => n.Address)
                .ThenInclude(a => a.CountryProvince)
                  .Include(n => n.Address)
                .ThenInclude(a => a.Country)
                   .Include(n => n.Address)
                .ThenInclude(a => a.Region)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (branch == null)
                throw new NotFoundException("branch not found");

            return new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                AdminComment = branch.AdminComment,

                Address = branch.Address == null ? null : new Addresses.Models.Dtos.AddressDto
                {
                    Id = branch.Address.Id,
                    Email = branch.Address.Email,
                    CountryId = branch.Address.CountryId,
                    CountryProvinceId = branch.Address.CountryProvinceId,
                    City = branch.Address.City?.Name,
                    CityId = branch.Address.CityId,
                    RegionId = branch.Address.RegionId,
                    Country = branch.Address.Country?.Name,
                    Region = branch.Address.Region?.Name,
                    CountryProvince = branch.Address.CountryProvince?.Name,
                    Address1 = branch.Address.Address1,
                    Address2 = branch.Address.Address2,
                    ZipPostalCode = branch.Address.ZipPostalCode,
                    PhoneNumber = branch.Address.PhoneNumber,
                    SecondaryPhoneNumber = branch.Address.SecondaryPhoneNumber,
                    FaxNumber = branch.Address.FaxNumber,
                    MapLink = branch.Address.MapLink
                },

            };
        }

        public async Task<bool> DeleteBranch(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
                throw new BadRequestException("Invalid branch.");

            branch.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region position.

        public async Task<int> InsertUpdatePosition(InsertUpdatePosition positionCmd)
        {

            if (positionCmd.Id == 0)
            {
                var newPosition = new Position
                {
                    AdminComment = positionCmd.AdminComment,
                    Name = positionCmd.Name,
                };

                _context.Positions.Add(newPosition);
                await _context.SaveChangesAsync();
                return newPosition.Id;
            }
            else
            {
                var existing = await _context.Positions
                                      .FirstOrDefaultAsync(i => i.Id == positionCmd.Id);
                if (existing == null)
                    throw new NotFoundException("position not found");

                existing.AdminComment = positionCmd.AdminComment;
                existing.Name = positionCmd.Name;


                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<PositionDto>> positionList()
        {
            return await _context.Positions
                .AsNoTracking()
                .Select(s => new PositionDto
                {
                    Id = s.Id,
                    AdminComment = s.AdminComment,
                    Name = s.Name,
                }).ToListAsync();
        }

        public async Task<PositionDto> GetPositionById(int id)
        {
            var position = await _context.Positions
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (position == null)
                throw new NotFoundException("position not found");

            return new PositionDto
            {
                Id = position.Id,
                Name = position.Name,
                AdminComment = position.AdminComment,

            };
        }

        public async Task<bool> DeletePosition(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
                throw new BadRequestException("Invalid position.");

            position.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Batch.

        public async Task<int> InsertUpdateBatch(InsertUpdateBatch batchCmd)
        {

            if (batchCmd.Id == 0)
            {
                var newBatch = new Batch
                {
                    AdminComment = batchCmd.AdminComment,
                    Name = batchCmd.Name,
                };

                _context.Batches.Add(newBatch);
                await _context.SaveChangesAsync();
                return newBatch.Id;
            }
            else
            {
                var existing = await _context.Batches
                                      .FirstOrDefaultAsync(i => i.Id == batchCmd.Id);
                if (existing == null)
                    throw new NotFoundException("batch not found");

                existing.AdminComment = batchCmd.AdminComment;
                existing.Name = batchCmd.Name;


                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<BatchDto>> BatchList()
        {
            return await _context.Positions
                .AsNoTracking()
                .Select(s => new BatchDto
                {
                    Id = s.Id,
                    AdminComment = s.AdminComment,
                    Name = s.Name,
                }).ToListAsync();
        }

        public async Task<BatchDto> GetBatchById(int id)
        {
            var batch = await _context.Batches
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (batch == null)
                throw new NotFoundException("batch not found");

            return new BatchDto
            {
                Id = batch.Id,
                Name = batch.Name,
                AdminComment = batch.AdminComment,

            };
        }

        public async Task<bool> DeleteBatch(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
                throw new BadRequestException("Invalid batch.");

            batch.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Grade.

        public async Task<int> InsertUpdateGrade(InsertUpdateGrade gardeCmd)
        {

            if (gardeCmd.Id == 0)
            {
                var newGrade = new Grade
                {
                    AdminComment = gardeCmd.AdminComment,
                    Name = gardeCmd.Name,
                };

                _context.Grades.Add(newGrade);
                await _context.SaveChangesAsync();
                return newGrade.Id;
            }
            else
            {
                var existing = await _context.Grades
                                      .FirstOrDefaultAsync(i => i.Id == gardeCmd.Id);
                if (existing == null)
                    throw new NotFoundException("grade not found");

                existing.AdminComment = gardeCmd.AdminComment;
                existing.Name = gardeCmd.Name;


                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<GradeDto>> GradeList()
        {
            return await _context.Grades
                .AsNoTracking()
                .Select(s => new GradeDto
                {
                    Id = s.Id,
                    AdminComment = s.AdminComment,
                    Name = s.Name,
                }).ToListAsync();
        }

        public async Task<GradeDto> GetGradeById(int id)
        {
            var grade = await _context.Grades
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (grade == null)
                throw new NotFoundException("grade not found");

            return new GradeDto
            {
                Id = grade.Id,
                Name = grade.Name,
                AdminComment = grade.AdminComment,

            };
        }

        public async Task<bool> DeleteGrade(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                throw new BadRequestException("Invalid grade.");

            grade.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

    }
}
