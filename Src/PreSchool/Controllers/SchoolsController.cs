using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Stores;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreSchool.Application.Services.Schools;
using PreSchool.Application.Services.Schools.Models.Commands;
using PreSchool.Application.Services.Schools.Models.Dtos;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {

        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        #region school

        [HttpPost]
       // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertSchool(InsertUpdateSchool schoolCmd)
        {
            if (schoolCmd == null)
                throw new BadRequestException(" School is required.");
            schoolCmd.Id = 0;
            return await _schoolService.InsertUpdateSchool(schoolCmd);
        }

        [HttpPut("{id}")]
       // [AuthorizeUser(Permissions.UpdateschoolCmds)]
        public async Task<int> UpdateSchool(int id, InsertUpdateSchool schoolCmd)
        {
            if (schoolCmd == null)
                throw new BadRequestException("School is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != schoolCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _schoolService.InsertUpdateSchool(schoolCmd);

        }

        [HttpGet]
        public async Task<List<SchoolListDto>> GetSchoolList()
        {
            return await _schoolService.GetSchoolList();

        }

        [HttpGet("{id}")]
        public async Task<SchoolDto> GetSchoolById(int id)
        {
            return await _schoolService.GetSchoolById(id);

        }

        #endregion

        #region branches

        [HttpPost("Branch")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertBranch(InsertUpdateBranch branchCmd)
        {
            if (branchCmd == null)
                throw new BadRequestException(" School branch is required.");
            branchCmd.Id = 0;
            return await _schoolService.InsertUpdateBranch(branchCmd);
        }

        [HttpPut("Branch/{id}")]
        // [AuthorizeUser(Permissions.UpdatebranchCmds)]
        public async Task<int> UpdateBranch(int id, InsertUpdateBranch branchCmd)
        {
            if (branchCmd == null)
                throw new BadRequestException("School branch is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != branchCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _schoolService.InsertUpdateBranch(branchCmd);

        }

        [HttpGet("Branch")]
        public async Task<List<BranchListDto>> BranchList()
        {
            return await _schoolService.BranchList();

        }

        [HttpGet("Branch/{id}")]
        public async Task<BranchDto> GetBranchById(int id)
        {
            return await _schoolService.GetBranchById(id);

        }
        [HttpDelete("Branch/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteBranch(int id)
        {
            return await _schoolService.DeleteBranch(id);

        }
        #endregion

        #region position

        [HttpPost("Position")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertPosition(InsertUpdatePosition positionCmd)
        {
            if (positionCmd == null)
                throw new BadRequestException(" Position is required.");
            positionCmd.Id = 0;
            return await _schoolService.InsertUpdatePosition(positionCmd);
        }

        [HttpPut("Position/{id}")]
        // [AuthorizeUser(Permissions.UpdatepositionCmds)]
        public async Task<int> UpdatePosition(int id, InsertUpdatePosition positionCmd)
        {
            if (positionCmd == null)
                throw new BadRequestException("Position is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != positionCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _schoolService.InsertUpdatePosition(positionCmd);

        }

        [HttpGet("Position")]
        public async Task<List<PositionDto>> positionList()
        {
            return await _schoolService.positionList();

        }

        [HttpGet("Position/{id}")]
        public async Task<PositionDto> GetPositionById(int id)
        {
            return await _schoolService.GetPositionById(id);

        }
        [HttpDelete("Position/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeletePosition(int id)
        {
            return await _schoolService.DeletePosition(id);

        }
        #endregion

        #region Batch

        [HttpPost("Batch")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertBatch(InsertUpdateBatch batchCmd)
        {
            if (batchCmd == null)
                throw new BadRequestException(" Batch is required.");
            batchCmd.Id = 0;
            return await _schoolService.InsertUpdateBatch(batchCmd);
        }

        [HttpPut("Batch/{id}")]
        // [AuthorizeUser(Permissions.UpdatebatchCmds)]
        public async Task<int> UpdateBatch(int id, InsertUpdateBatch batchCmd)
        {
            if (batchCmd == null)
                throw new BadRequestException("Batch is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != batchCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _schoolService.InsertUpdateBatch(batchCmd);

        }

        [HttpGet("Batch")]
        public async Task<List<BatchDto>> BatchList()
        {
            return await _schoolService.BatchList();

        }

        [HttpGet("Batch/{id}")]
        public async Task<BatchDto> GetBatchById(int id)
        {
            return await _schoolService.GetBatchById(id);

        }
        [HttpDelete("Batch/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteBatch(int id)
        {
            return await _schoolService.DeleteBatch(id);

        }
        #endregion

        #region Grade

        [HttpPost("Grade")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertGrade(InsertUpdateGrade gardeCmd)
        {
            if (gardeCmd == null)
                throw new BadRequestException(" Grade is required.");
            gardeCmd.Id = 0;
            return await _schoolService.InsertUpdateGrade(gardeCmd);
        }

        [HttpPut("Grade/{id}")]
        // [AuthorizeUser(Permissions.UpdategardeCmds)]
        public async Task<int> UpdateGrade(int id, InsertUpdateGrade gardeCmd)
        {
            if (gardeCmd == null)
                throw new BadRequestException("Grade is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != gardeCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _schoolService.InsertUpdateGrade(gardeCmd);

        }

        [HttpGet("Grade")]
        public async Task<List<GradeDto>> GradeList()
        {
            return await _schoolService.GradeList();

        }

        [HttpGet("Grade/{id}")]
        public async Task<GradeDto> GetGradeById(int id)
        {
            return await _schoolService.GetGradeById(id);

        }
        [HttpDelete("Grade/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteGrade(int id)
        {
            return await _schoolService.DeleteGrade(id);

        }
        #endregion



    }
}
