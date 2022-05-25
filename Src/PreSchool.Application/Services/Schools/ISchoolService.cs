using PreSchool.Application.Services.Schools.Models.Commands;
using PreSchool.Application.Services.Schools.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Schools
{
    public interface ISchoolService
    {
        Task<List<BatchDto>> BatchList();
        Task<List<BranchListDto>> BranchList();
        Task<bool> DeleteBatch(int id);
        Task<bool> DeleteBranch(int id);
        Task<bool> DeleteGrade(int id);
        Task<bool> DeletePosition(int id);
        Task<BatchDto> GetBatchById(int id);
        Task<BranchDto> GetBranchById(int id);
        Task<GradeDto> GetGradeById(int id);
        Task<PositionDto> GetPositionById(int id);
        Task<SchoolDto> GetSchoolById(int id);
        Task<List<SchoolListDto>> GetSchoolList();
        Task<List<GradeDto>> GradeList();
        Task<int> InsertUpdateBatch(InsertUpdateBatch batchCmd);
        Task<int> InsertUpdateBranch(InsertUpdateBranch branchCmd);
        Task<int> InsertUpdateGrade(InsertUpdateGrade gardeCmd);
        Task<int> InsertUpdatePosition(InsertUpdatePosition positionCmd);
        Task<int> InsertUpdateSchool(InsertUpdateSchool schoolCmd);
        Task<List<PositionDto>> positionList();
    }
}