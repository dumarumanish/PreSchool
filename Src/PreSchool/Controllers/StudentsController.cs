using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Stores;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreSchool.Application.Services.Students;
using PreSchool.Application.Services.Students.Models.Dtos;
using PreSchool.Application.Services.Students.Models.Queries;
using PreSchool.Application;
using PreSchool.Application.Services.Students.Models.Commands;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        #region StudentEnquiry.

        [HttpPost("Enquiry")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertStudentEnquiry(InsertUpdateStudentEnquiry studentCmd)
        {
            if (studentCmd == null)
                throw new BadRequestException(" student Enquiry is required.");
            studentCmd.Id = 0;
            return await _studentService.InsertUpdateStudentEnquiry(studentCmd);
        }

        [HttpPut("Enquiry/{id}")]
        // [AuthorizeUser(Permissions.UpdatestudentCmds)]
        public async Task<int> UpdateStudentEnquiry(int id, InsertUpdateStudentEnquiry studentCmd)
        {
            if (studentCmd == null)
                throw new BadRequestException("student Enquiry is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != studentCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _studentService.InsertUpdateStudentEnquiry(studentCmd);

        }

        [HttpGet("Enquiry")]
        public async Task<PagedResult<StudentEnquiryListDto>> StudentEnquiryList([FromQuery] StudentEnquiryFilter filter)
        {
            return await _studentService.StudentEnquiryList(filter);

        }

        [HttpGet("Enquiry/{id}")]
        public async Task<StudentEnquiryDto> GetStudentEnquiryById(int id)
        {
            return await _studentService.GetStudentEnquiryById(id);

        }
        [HttpDelete("Enquiry/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteStudentEnquiry(int id)
        {
            return await _studentService.DeleteStudentEnquiry(id);

        }

        #region student enquery Images

        [HttpPost("{categoryId}/Thumbnails")]
       // [AuthorizeUser(Permissions.AddCategories, Permissions.UpdateCategories)]
        public async Task<bool> InsertStudentEnquiryImage(int categoryId, [FromForm] InsertStudentEnquiryImage image)
        {
            if (image == null)
                throw new BadRequestException("Image is required");

            if (categoryId != image.studentEnquiryId)
                throw new BadRequestException("Invalid studentEnquiry id, studentEnquiry id doesnot match");


            return await _studentService.InsertStudentEnquiryImage(image);
        }



        #endregion
        #endregion

        #region Student Registration.

        [HttpPost("Registration")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertStudentRegistration(InsertUpdateStudentRegistration studentCmd)
        {
            if (studentCmd == null)
                throw new BadRequestException(" student Registration is required.");
            studentCmd.Id = 0;
            return await _studentService.InsertUpdateStudentRegistration(studentCmd);
        }

        [HttpPut("Registration/{id}")]
        // [AuthorizeUser(Permissions.UpdatestudentCmds)]
        public async Task<int> UpdateStudentRegistration(int id, InsertUpdateStudentRegistration studentCmd)
        {
            if (studentCmd == null)
                throw new BadRequestException("student Registration is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != studentCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _studentService.InsertUpdateStudentRegistration(studentCmd);

        }

        [HttpGet("Registration")]
        public async Task<PagedResult<StudentRegistrationListDto>> StudentRegistrationList([FromQuery] StudentRegistrationFilter filter)
        {
            return await _studentService.StudentRegistrationList(filter);

        }

        [HttpGet("Registration/{id}")]
        public async Task<StudentRegistrationDto> GetStudentRegistrationById(int id)
        {
            return await _studentService.GetStudentRegistrationById(id);

        }
        [HttpDelete("Registration/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteStudentRegistration(int id)
        {
            return await _studentService.DeleteStudentRegistration(id);

        }
        #region student Registration Images

        [HttpPost("{categoryId}/Thumbnails")]
        // [AuthorizeUser(Permissions.AddCategories, Permissions.UpdateCategories)]
        public async Task<bool> InsertStudentRegistrationImage(int categoryId, [FromForm] InsertStudentRegistrationImage image)
        {
            if (image == null)
                throw new BadRequestException("Image is required");

            if (categoryId != image.studentRegistrationId)
                throw new BadRequestException("Invalid studentRegistration id, studentRegistration id doesnot match");


            return await _studentService.InsertStudentRegistrationImage(image);
        }



        #endregion
        #endregion

        #region student fees.

        [HttpPost("Fee")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertFee(InsertUpdateFee studentFeeCmd)
        {
            if (studentFeeCmd == null)
                throw new BadRequestException(" student Fee is required.");
            studentFeeCmd.Id = 0;
            return await _studentService.InsertUpdateFee(studentFeeCmd);
        }

        [HttpPut("Fee/{id}")]
        // [AuthorizeUser(Permissions.UpdatestudentFeeCmds)]
        public async Task<int> UpdateFee(int id, InsertUpdateFee studentFeeCmd)
        {
            if (studentFeeCmd == null)
                throw new BadRequestException("student Fee is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != studentFeeCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _studentService.InsertUpdateFee(studentFeeCmd);

        }

        [HttpGet("Fee")]
        public async Task<List<FeeDto>> FeeList()
        {
            return await _studentService.FeeList();

        }


        [HttpGet("Fee/{id}")]
        public async Task<FeeDto> FeeById(int id)
        {
            return await _studentService.FeeById(id);

        }
        [HttpDelete("Fee/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteFee(int id)
        {
            return await _studentService.DeleteFee(id);

        }

        #endregion

        #region Additional student fees.

        [HttpPost("Additional/Fee")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertAdditionalFee(InsertUpdateAdditionalFee studentFeeCmd)
        {
            if (studentFeeCmd == null)
                throw new BadRequestException(" student Additional Fee is required.");
            studentFeeCmd.Id = 0;
            return await _studentService.InsertUpdateAdditionalFee(studentFeeCmd);
        }

        [HttpPut("Additional/Fee/{id}")]
        // [AuthorizeUser(Permissions.UpdatestudentFeeCmds)]
        public async Task<int> UpdateAdditionalFee(int id, InsertUpdateAdditionalFee studentFeeCmd)
        {
            if (studentFeeCmd == null)
                throw new BadRequestException("student Additional Fee is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != studentFeeCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _studentService.InsertUpdateAdditionalFee(studentFeeCmd);

        }

        [HttpGet("Additional/Fee")]
        public async Task<List<AdditionalFeeDto>> AdditionalFeeList()
        {
            return await _studentService.AdditionalFeeList();

        }


        [HttpGet("Additional/Fee/{id}")]
        public async Task<AdditionalFeeDto> AdditionalFeeById(int id)
        {
            return await _studentService.AdditionalFeeById(id);

        }
        [HttpDelete("Additional/Fee/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteAdditionalFee(int id)
        {
            return await _studentService.DeleteAdditionalFee(id);

        }

        #endregion

        #region student fees payment.

        [HttpPost("Fee/Payment")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertFeePayment(InsertUpdateFeePayment studentFeeCmd)
        {
            if (studentFeeCmd == null)
                throw new BadRequestException(" student Fee payment is required.");
            studentFeeCmd.Id = 0;
            return await _studentService.InsertUpdateFeePayment(studentFeeCmd);
        }

        [HttpPut("Fee/Payment/{id}")]
        // [AuthorizeUser(Permissions.UpdatestudentFeeCmds)]
        public async Task<int> UpdateFeePayment(int id, InsertUpdateFeePayment studentFeeCmd)
        {
            if (studentFeeCmd == null)
                throw new BadRequestException("student Fee payment is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != studentFeeCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _studentService.InsertUpdateFeePayment(studentFeeCmd);

        }

        [HttpGet("Fee/Payment")]
        public async Task<List<FeePaymentDto>> StudentFeePaymentList()
        {
            return await _studentService.StudentFeePaymentList();

        }


        [HttpGet("Fee/Payment/{id}")]
        public async Task<FeePaymentDto> StudentFeePaymentById(int id)
        {
            return await _studentService.StudentFeePaymentById(id);

        }
        [HttpDelete("Fee/Payment/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteFeePayment(int id)
        {
            return await _studentService.DeleteFeePayment(id);

        }

        #endregion

        #region bad dept.

        [HttpPost("BadDept")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertBadDept(InsertUpdateBadDept studentFeeCmd)
        {
            if (studentFeeCmd == null)
                throw new BadRequestException(" student BadDept is required.");
            studentFeeCmd.Id = 0;
            return await _studentService.InsertUpdateBadDept(studentFeeCmd);
        }

        [HttpPut("BadDept/{id}")]
        // [AuthorizeUser(Permissions.UpdatestudentFeeCmds)]
        public async Task<int> UpdateBadDept(int id, InsertUpdateBadDept studentFeeCmd)
        {
            if (studentFeeCmd == null)
                throw new BadRequestException("student BadDept is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != studentFeeCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _studentService.InsertUpdateBadDept(studentFeeCmd);

        }

        [HttpGet("BadDept")]
        public async Task<List<BadDeptDto>> BadDeptList()
        {
            return await _studentService.BadDeptList();

        }


        [HttpGet("BadDept/{id}")]
        public async Task<BadDeptDto> BadDeptById(int id)
        {
            return await _studentService.BadDeptById(id);

        }
        [HttpDelete("BadDept/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteBadDept(int id)
        {
            return await _studentService.DeleteBadDept(id);

        }

        #endregion

    }
}
