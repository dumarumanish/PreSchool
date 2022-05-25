using PreSchool.Application.Services.Students.Models.Commands;
using PreSchool.Application.Services.Students.Models.Dtos;
using PreSchool.Application.Services.Students.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Students
{
    public interface IStudentService
    {
        Task<AdditionalFeeDto> AdditionalFeeById(int id);
        Task<List<AdditionalFeeDto>> AdditionalFeeList();
        Task<BadDeptDto> BadDeptById(int id);
        Task<List<BadDeptDto>> BadDeptList();
        Task<bool> DeleteAdditionalFee(int id);
        Task<bool> DeleteBadDept(int id);
        Task<bool> DeleteFee(int id);
        Task<bool> DeleteFeePayment(int id);
        Task<bool> DeleteStudentEnquiry(int id);
        Task<bool> DeleteStudentRegistration(int id);
        Task<FeeDto> FeeById(int id);
        Task<List<FeeDto>> FeeList();
        Task<StudentEnquiryDto> GetStudentEnquiryById(int id);
        Task<StudentRegistrationDto> GetStudentRegistrationById(int id);
        Task<bool> InsertStudentEnquiryImage(InsertStudentEnquiryImage image);
        Task<bool> InsertStudentRegistrationImage(InsertStudentRegistrationImage image);
        Task<int> InsertUpdateAdditionalFee(InsertUpdateAdditionalFee studentFeeCmd);
        Task<int> InsertUpdateBadDept(InsertUpdateBadDept studentFeeCmd);
        Task<int> InsertUpdateFee(InsertUpdateFee studentFeeCmd);
        Task<int> InsertUpdateFeePayment(InsertUpdateFeePayment studentFeeCmd);
        Task<int> InsertUpdateStudentEnquiry(InsertUpdateStudentEnquiry studentCmd);
        Task<int> InsertUpdateStudentRegistration(InsertUpdateStudentRegistration studentCmd);
        Task<PagedResult<StudentEnquiryListDto>> StudentEnquiryList(StudentEnquiryFilter filter);
        Task<FeePaymentDto> StudentFeePaymentById(int id);
        Task<List<FeePaymentDto>> StudentFeePaymentList();
        Task<PagedResult<StudentRegistrationListDto>> StudentRegistrationList(StudentRegistrationFilter filter);
    }
}