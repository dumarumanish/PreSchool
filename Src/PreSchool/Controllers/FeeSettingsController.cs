using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Stores;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreSchool.Application.Services.FeeSettings;
using PreSchool.Application.Services.FeeSettings.Models.Commands;
using PreSchool.Application.Services.FeeSettings.Models.Dtos;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeSettingsController : ControllerBase
    {

        private readonly IFeeSettingService _feeSettingService;

        public FeeSettingsController(IFeeSettingService feeSettingService)
        {
            _feeSettingService = feeSettingService;
        }

        #region month

        [HttpPost("Month")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertMonth(InsertUpdateMonth monthCmd)
        {
            if (monthCmd == null)
                throw new BadRequestException("Month is required.");
            monthCmd.Id = 0;
            return await _feeSettingService.InsertUpdateMonth(monthCmd);
        }

        [HttpPut("Month/{id}")]
        // [AuthorizeUser(Permissions.UpdatemonthCmds)]
        public async Task<int> UpdateMonth(int id, InsertUpdateMonth monthCmd)
        {
            if (monthCmd == null)
                throw new BadRequestException("Month is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != monthCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _feeSettingService.InsertUpdateMonth(monthCmd);

        }

        [HttpGet("Month")]
        public async Task<List<MonthDto>> MonthList()
        {
            return await _feeSettingService.MonthList();

        }

        [HttpGet("Month/{id}")]
        public async Task<MonthDto> GetMonthById(int id)
        {
            return await _feeSettingService.GetMonthById(id);

        }
        [HttpDelete("Month/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteMonth(int id)
        {
            return await _feeSettingService.DeleteMonth(id);

        }
        #endregion

        #region Bill Period.

        [HttpPost("BillPeriod")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertBillPeriod(InsertUpdateBillPeriod billPeriodCmd)
        {
            if (billPeriodCmd == null)
                throw new BadRequestException("bill period is required.");
            billPeriodCmd.Id = 0;
            return await _feeSettingService.InsertUpdateBillPeriod(billPeriodCmd);
        }

        [HttpPut("BillPeriod/{id}")]
        // [AuthorizeUser(Permissions.UpdatebillPeriodCmds)]
        public async Task<int> UpdateBillPeriod(int id, InsertUpdateBillPeriod billPeriodCmd)
        {
            if (billPeriodCmd == null)
                throw new BadRequestException("bill period is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != billPeriodCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _feeSettingService.InsertUpdateBillPeriod(billPeriodCmd);

        }

        [HttpGet("BillPeriod")]
        public async Task<List<BillPeriodDto>> BillPeriodList()
        {
            return await _feeSettingService.BillPeriodList();

        }

        [HttpGet("BillPeriod/{id}")]
        public async Task<BillPeriodDto> GetBillPeriodById(int id)
        {
            return await _feeSettingService.GetBillPeriodById(id);

        }
        [HttpDelete("BillPeriod/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteBillPeriod(int id)
        {
            return await _feeSettingService.DeleteBillPeriod(id);

        }
        #endregion

        #region Fee Group.

        [HttpPost("FeeGroup")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertFeeGroup(InsertUpdateFeeGroup feeGroupCmd)
        {
            if (feeGroupCmd == null)
                throw new BadRequestException("FeeGroup is required.");
            feeGroupCmd.Id = 0;
            return await _feeSettingService.InsertUpdateFeeGroup(feeGroupCmd);
        }

        [HttpPut("FeeGroup/{id}")]
        // [AuthorizeUser(Permissions.UpdatefeeGroupCmds)]
        public async Task<int> UpdateFeeGroup(int id, InsertUpdateFeeGroup feeGroupCmd)
        {
            if (feeGroupCmd == null)
                throw new BadRequestException("FeeGroup is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != feeGroupCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _feeSettingService.InsertUpdateFeeGroup(feeGroupCmd);

        }

        [HttpGet("FeeGroup")]
        public async Task<List<FeeGroupDto>> FeeGroupList()
        {
            return await _feeSettingService.FeeGroupList();

        }

        [HttpGet("FeeGroup/{id}")]
        public async Task<FeeGroupDto> GetFeeGroupById(int id)
        {
            return await _feeSettingService.GetFeeGroupById(id);

        }
        [HttpDelete("FeeGroup/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteFeeGroup(int id)
        {
            return await _feeSettingService.DeleteFeeGroup(id);

        }
        #endregion

        #region Fee Title

        [HttpPost("FeeTitle")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> UpdateFeeTitle(InsertUpdateFeeTitle feeTitleCmd)
        {
            if (feeTitleCmd == null)
                throw new BadRequestException("FeeTitle is required.");
            feeTitleCmd.Id = 0;
            return await _feeSettingService.InsertUpdateFeeTitle(feeTitleCmd);
        }

        [HttpPut("FeeTitle/{id}")]
        // [AuthorizeUser(Permissions.UpdatefeeTitleCmds)]
        public async Task<int> UpdateFeeTitle(int id, InsertUpdateFeeTitle feeTitleCmd)
        {
            if (feeTitleCmd == null)
                throw new BadRequestException("FeeTitle is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != feeTitleCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _feeSettingService.InsertUpdateFeeTitle(feeTitleCmd);

        }

        [HttpGet("FeeTitle")]
        public async Task<List<FeeTitleDto>> FeeTitleList()
        {
            return await _feeSettingService.FeeTitleList();

        }

        [HttpGet("FeeTitle/{id}")]
        public async Task<FeeTitleDto> GetFeeTitleById(int id)
        {
            return await _feeSettingService.GetFeeTitleById(id);

        }
        [HttpDelete("FeeTitle/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteFeeTitle(int id)
        {
            return await _feeSettingService.DeleteFeeTitle(id);

        }
        #endregion

        #region Fee rate.

        [HttpPost("FeeRate")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertFeeRate(InsertUpdateFeeRate feeRateCmd)
        {
            if (feeRateCmd == null)
                throw new BadRequestException("FeeRate is required.");
            feeRateCmd.Id = 0;
            return await _feeSettingService.InsertUpdateFeeRate(feeRateCmd);
        }

        [HttpPut("FeeRate/{id}")]
        // [AuthorizeUser(Permissions.UpdatefeeRateCmds)]
        public async Task<int> UpdateFeeRate(int id, InsertUpdateFeeRate feeRateCmd)
        {
            if (feeRateCmd == null)
                throw new BadRequestException("FeeRate is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != feeRateCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _feeSettingService.InsertUpdateFeeRate(feeRateCmd);

        }

        [HttpGet("FeeRate")]
        public async Task<List<FeeRateDto>> FeeRateList()
        {
            return await _feeSettingService.FeeRateList();

        }

        [HttpGet("FeeRate/{id}")]
        public async Task<FeeRateDto> GetFeeRateById(int id)
        {
            return await _feeSettingService.GetFeeRateById(id);

        }
        [HttpDelete("FeeRate/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteFeeRate(int id)
        {
            return await _feeSettingService.DeleteFeeRate(id);

        }
        #endregion

        #region student fee.

        [HttpPost("Student/Fee")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertStudentFee(InsertUpdateStudentFee feeCmd)
        {
            if (feeCmd == null)
                throw new BadRequestException("student fee is required.");
            feeCmd.Id = 0;
            return await _feeSettingService.InsertUpdateStudentFee(feeCmd);
        }

        [HttpPut("Student/Fee/{id}")]
        // [AuthorizeUser(Permissions.UpdatefeeCmds)]
        public async Task<int> UpdateStudentFee(int id, InsertUpdateStudentFee feeCmd)
        {
            if (feeCmd == null)
                throw new BadRequestException("student fee is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != feeCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _feeSettingService.InsertUpdateStudentFee(feeCmd);

        }

        [HttpGet("Student/Fee")]
        public async Task<List<StudentFeeDto>> StudentFeeList()
        {
            return await _feeSettingService.StudentFeeList();

        }

        [HttpGet("FeeRate/{id}")]
        public async Task<StudentFeeDto> GetStudentFeeById(int id)
        {
            return await _feeSettingService.GetStudentFeeById(id);

        }
        [HttpDelete("Student/Fee/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteStudentFee(int id)
        {
            return await _feeSettingService.DeleteStudentFee(id);

        }
        #endregion

        #region Discount.

        [HttpPost("Discount")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertDiscount(InsertUpdateDiscount discountCmd)
        {
            if (discountCmd == null)
                throw new BadRequestException("Discount is required.");
            discountCmd.Id = 0;
            return await _feeSettingService.InsertUpdateDiscount(discountCmd);
        }

        [HttpPut("Discount/{id}")]
        // [AuthorizeUser(Permissions.UpdatediscountCmds)]
        public async Task<int> UpdateDiscount(int id, InsertUpdateDiscount discountCmd)
        {
            if (discountCmd == null)
                throw new BadRequestException("Discount is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != discountCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _feeSettingService.InsertUpdateDiscount(discountCmd);

        }

        [HttpGet("Discount")]
        public async Task<List<DiscountDto>> DiscountList()
        {
            return await _feeSettingService.DiscountList();

        }

        [HttpGet("Discount/{id}")]
        public async Task<DiscountDto> DiscountById(int id)
        {
            return await _feeSettingService.DiscountById(id);

        }
        [HttpDelete("Discount/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteDiscount(int id)
        {
            return await _feeSettingService.DeleteDiscount(id);

        }
        #endregion

        #region Opening Balance.

        [HttpPost("Opening/Balance")]
        // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertOpeningBalance(InsertUpdateOpeningBalance openingBalanceCmd)
        {
            if (openingBalanceCmd == null)
                throw new BadRequestException("opening balance is required.");
            openingBalanceCmd.Id = 0;
            return await _feeSettingService.InsertUpdateOpeningBalance(openingBalanceCmd);
        }

        [HttpPut("Opening/Balance/{id}")]
        // [AuthorizeUser(Permissions.UpdateopeningBalanceCmds)]
        public async Task<int> UpdateOpeningBalance(int id, InsertUpdateOpeningBalance openingBalanceCmd)
        {
            if (openingBalanceCmd == null)
                throw new BadRequestException("opening balance is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != openingBalanceCmd.Id)
                throw new BadRequestException("Id doesnot match");
            return await _feeSettingService.InsertUpdateOpeningBalance(openingBalanceCmd);

        }

        [HttpGet("Opening/Balance")]
        public async Task<List<OpeningBalanceDto>> OpeningBalanceList()
        {
            return await _feeSettingService.OpeningBalanceList();

        }

        [HttpGet("Opening/Balance/{id}")]
        public async Task<OpeningBalanceDto> OpeningBalanceById(int id)
        {
            return await _feeSettingService.OpeningBalanceById(id);

        }
        [HttpDelete("Opening/Balance/{id}")]
        //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteOpeninigBalance(int id)
        {
            return await _feeSettingService.DeleteOpeninigBalance(id);

        }
        #endregion


    }
}
