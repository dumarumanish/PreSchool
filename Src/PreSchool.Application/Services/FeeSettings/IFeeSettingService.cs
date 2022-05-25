using PreSchool.Application.Services.FeeSettings.Models.Commands;
using PreSchool.Application.Services.FeeSettings.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.FeeSettings
{
    public interface IFeeSettingService
    {
        Task<List<BillPeriodDto>> BillPeriodList();
        Task<bool> DeleteBillPeriod(int id);
        Task<bool> DeleteDiscount(int id);
        Task<bool> DeleteFeeGroup(int id);
        Task<bool> DeleteFeeRate(int id);
        Task<bool> DeleteFeeTitle(int id);
        Task<bool> DeleteMonth(int id);
        Task<bool> DeleteOpeninigBalance(int id);
        Task<bool> DeleteStudentFee(int id);
        Task<DiscountDto> DiscountById(int id);
        Task<List<DiscountDto>> DiscountList();
        Task<List<FeeGroupDto>> FeeGroupList();
        Task<List<FeeRateDto>> FeeRateList();
        Task<List<FeeTitleDto>> FeeTitleList();
        Task<BillPeriodDto> GetBillPeriodById(int id);
        Task<FeeGroupDto> GetFeeGroupById(int id);
        Task<FeeRateDto> GetFeeRateById(int id);
        Task<FeeTitleDto> GetFeeTitleById(int id);
        Task<MonthDto> GetMonthById(int id);
        Task<StudentFeeDto> GetStudentFeeById(int id);
        Task<int> InsertUpdateBillPeriod(InsertUpdateBillPeriod billPeriodCmd);
        Task<int> InsertUpdateDiscount(InsertUpdateDiscount discountCmd);
        Task<int> InsertUpdateFeeGroup(InsertUpdateFeeGroup feeGroupCmd);
        Task<int> InsertUpdateFeeRate(InsertUpdateFeeRate feeRateCmd);
        Task<int> InsertUpdateFeeTitle(InsertUpdateFeeTitle feeTitleCmd);
        Task<int> InsertUpdateMonth(InsertUpdateMonth monthCmd);
        Task<int> InsertUpdateOpeningBalance(InsertUpdateOpeningBalance openingBalanceCmd);
        Task<int> InsertUpdateStudentFee(InsertUpdateStudentFee feeCmd);
        Task<List<MonthDto>> MonthList();
        Task<OpeningBalanceDto> OpeningBalanceById(int id);
        Task<List<OpeningBalanceDto>> OpeningBalanceList();
        Task<List<StudentFeeDto>> StudentFeeList();
    }
}