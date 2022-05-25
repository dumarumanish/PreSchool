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
using PreSchool.Application.Services.FeeSettings.Models.Commands;
using PreSchool.Data.Entities.FeeSettings;
using PreSchool.Application.Services.FeeSettings.Models.Dtos;

namespace PreSchool.Application.Services.FeeSettings
{
    public class FeeSettingService : IFeeSettingService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;
        private readonly IAppFeatureService _appFeatureService;

        public FeeSettingService(
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

        #region month.

        public async Task<int> InsertUpdateMonth(InsertUpdateMonth monthCmd)
        {

            if (monthCmd.Id == 0)
            {
                var newMonth = new Month
                {
                    AdminComment = monthCmd.AdminComment,
                    Name = monthCmd.Name,
                };
                _context.Months.Add(newMonth);
                await _context.SaveChangesAsync();
                return newMonth.Id;
            }
            else
            {
                var existing = await _context.Months
                                      .FirstOrDefaultAsync(i => i.Id == monthCmd.Id);
                if (existing == null)
                    throw new NotFoundException("month not found");

                existing.AdminComment = monthCmd.AdminComment;
                existing.Name = monthCmd.Name;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<MonthDto>> MonthList()
        {
            return await _context.Branches
                .AsNoTracking()
                .Select(s => new MonthDto
                {
                    Id = s.Id,
                    AdminComment = s.AdminComment,
                    Name = s.Name,
                }).ToListAsync();
        }

        public async Task<MonthDto> GetMonthById(int id)
        {
            var month = await _context.Months
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (month == null)
                throw new NotFoundException("month not found");

            return new MonthDto
            {
                Id = month.Id,
                Name = month.Name,
                AdminComment = month.AdminComment,

            };
        }

        public async Task<bool> DeleteMonth(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var month = await _context.Months.FindAsync(id);
            if (month == null)
                throw new BadRequestException("Invalid month.");

            month.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Bill Period.

        public async Task<int> InsertUpdateBillPeriod(InsertUpdateBillPeriod billPeriodCmd)
        {

            if (billPeriodCmd.Id == 0)
            {
                var newBillPeriod = new BillPeriod
                {
                    IsActive = billPeriodCmd.IsActive,
                    Name = billPeriodCmd.Name,
                    ShortDescription = billPeriodCmd.ShortDescription,
                    UniqueCode = billPeriodCmd.UniqueCode,
                };
                _context.BillPeriods.Add(newBillPeriod);
                await _context.SaveChangesAsync();
                return newBillPeriod.Id;
            }
            else
            {
                var existing = await _context.BillPeriods
                                      .FirstOrDefaultAsync(i => i.Id == billPeriodCmd.Id);
                if (existing == null)
                    throw new NotFoundException("BillPeriod not found");

                existing.IsActive = billPeriodCmd.IsActive;
                existing.Name = billPeriodCmd.Name;
                existing.ShortDescription = billPeriodCmd.ShortDescription;
                existing.UniqueCode = billPeriodCmd.UniqueCode;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<BillPeriodDto>> BillPeriodList()
        {
            return await _context.BillPeriods
                .AsNoTracking()
                .Select(s => new BillPeriodDto
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    Name = s.Name,
                    ShortDescription = s.ShortDescription,
                    UniqueCode = s.UniqueCode,
                }).ToListAsync();
        }

        public async Task<BillPeriodDto> GetBillPeriodById(int id)
        {
            var billPeriod = await _context.BillPeriods
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (billPeriod == null)
                throw new NotFoundException("billPeriod not found");

            return new BillPeriodDto
            {
                Id = billPeriod.Id,
                IsActive = billPeriod.IsActive,
                Name = billPeriod.Name,
                ShortDescription = billPeriod.ShortDescription,
                UniqueCode = billPeriod.UniqueCode,

            };
        }

        public async Task<bool> DeleteBillPeriod(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var billPeriod = await _context.BillPeriods.FindAsync(id);
            if (billPeriod == null)
                throw new BadRequestException("Invalid billPeriod.");

            billPeriod.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Fee Group.

        public async Task<int> InsertUpdateFeeGroup(InsertUpdateFeeGroup feeGroupCmd)
        {

            if (feeGroupCmd.Id == 0)
            {
                var newFeeGroup = new FeeGroup
                {
                    IsActive = feeGroupCmd.IsActive,
                    Name = feeGroupCmd.Name,
                    ShortDescription = feeGroupCmd.ShortDescription,
                    UniqueCode = feeGroupCmd.UniqueCode,
                    BillPeriodId = feeGroupCmd.BillPeriodId,
                };
                _context.FeeGroups.Add(newFeeGroup);
                await _context.SaveChangesAsync();
                return newFeeGroup.Id;
            }
            else
            {
                var existing = await _context.FeeGroups
                                      .FirstOrDefaultAsync(i => i.Id == feeGroupCmd.Id);
                if (existing == null)
                    throw new NotFoundException("fee group not found");

                existing.IsActive = feeGroupCmd.IsActive;
                existing.Name = feeGroupCmd.Name;
                existing.ShortDescription = feeGroupCmd.ShortDescription;
                existing.UniqueCode = feeGroupCmd.UniqueCode;
                existing.BillPeriodId = feeGroupCmd.BillPeriodId;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<FeeGroupDto>> FeeGroupList()
        {
            return await _context.FeeGroups
                .Include(b => b.BillPeriod)
                .AsNoTracking()
                .Select(s => new FeeGroupDto
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    Name = s.Name,
                    ShortDescription = s.ShortDescription,
                    UniqueCode = s.UniqueCode,
                    BillPeriodId = s.BillPeriodId,
                    BillPeriodName = s.BillPeriod.Name,
                }).ToListAsync();
        }

        public async Task<FeeGroupDto> GetFeeGroupById(int id)
        {
            var feeGroup = await _context.FeeGroups
                .Include(b => b.BillPeriod)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (feeGroup == null)
                throw new NotFoundException("feeGroup not found");

            return new FeeGroupDto
            {
                Id = feeGroup.Id,
                IsActive = feeGroup.IsActive,
                Name = feeGroup.Name,
                ShortDescription = feeGroup.ShortDescription,
                UniqueCode = feeGroup.UniqueCode,
                BillPeriodId = feeGroup.BillPeriodId,
                BillPeriodName = feeGroup.BillPeriod.Name,

            };
        }

        public async Task<bool> DeleteFeeGroup(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var feeGroup = await _context.FeeGroups.FindAsync(id);
            if (feeGroup == null)
                throw new BadRequestException("Invalid feeGroup.");

            feeGroup.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Fee Title.

        public async Task<int> InsertUpdateFeeTitle(InsertUpdateFeeTitle feeTitleCmd)
        {

            if (feeTitleCmd.Id == 0)
            {
                var newFeeTitle = new FeeTitle
                {
                    IsActive = feeTitleCmd.IsActive,
                    Name = feeTitleCmd.Name,
                    ShortDescription = feeTitleCmd.ShortDescription,
                    FeeGroupId = feeTitleCmd.FeeGroupId,
                    FeeTypeId = feeTitleCmd.FeeTypeId,
                    IsDiscountApplicable = feeTitleCmd.IsDiscountApplicable,
                    IsTaxable = feeTitleCmd.IsTaxable,
                    UniqueId = feeTitleCmd.UniqueId,
                };
                _context.FeeTitles.Add(newFeeTitle);
                await _context.SaveChangesAsync();
                return newFeeTitle.Id;
            }
            else
            {
                var existing = await _context.FeeTitles
                                      .FirstOrDefaultAsync(i => i.Id == feeTitleCmd.Id);
                if (existing == null)
                    throw new NotFoundException("fee title not found");

                existing.IsActive = feeTitleCmd.IsActive;
                existing.Name = feeTitleCmd.Name;
                existing.ShortDescription = feeTitleCmd.ShortDescription;
                existing.IsDiscountApplicable = feeTitleCmd.IsDiscountApplicable;
                existing.IsTaxable = feeTitleCmd.IsTaxable;
                existing.UniqueId = feeTitleCmd.UniqueId;
                existing.FeeGroupId = feeTitleCmd.FeeGroupId;
                existing.FeeTypeId = feeTitleCmd.FeeTypeId;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<FeeTitleDto>> FeeTitleList()
        {
            return await _context.FeeTitles
                .Include(b => b.FeeGroup)
                .AsNoTracking()
                .Select(s => new FeeTitleDto
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    Name = s.Name,
                    ShortDescription = s.ShortDescription,
                    FeeTypeId = s.FeeTypeId,
                    UniqueId = s.UniqueId,
                    IsDiscountApplicable = s.IsDiscountApplicable,
                    IsTaxable = s.IsTaxable,
                    FeeGroupId = s.FeeGroupId,
                    FeeGroupname = s.FeeGroup.Name,
                }).ToListAsync();
        }

        public async Task<FeeTitleDto> GetFeeTitleById(int id)
        {
            var feeTitle = await _context.FeeTitles
                .Include(b => b.FeeGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (feeTitle == null)
                throw new NotFoundException("feeTitle not found");

            return new FeeTitleDto
            {
                Id = feeTitle.Id,
                IsActive = feeTitle.IsActive,
                Name = feeTitle.Name,
                ShortDescription = feeTitle.ShortDescription,
                FeeTypeId = feeTitle.FeeTypeId,
                UniqueId = feeTitle.UniqueId,
                IsDiscountApplicable = feeTitle.IsDiscountApplicable,
                IsTaxable = feeTitle.IsTaxable,
                FeeGroupId = feeTitle.FeeGroupId,
                FeeGroupname = feeTitle.FeeGroup.Name,

            };
        }

        public async Task<bool> DeleteFeeTitle(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var feeTitle = await _context.FeeTitles.FindAsync(id);
            if (feeTitle == null)
                throw new BadRequestException("Invalid feeTitle.");

            feeTitle.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region fee rate.

        public async Task<int> InsertUpdateFeeRate(InsertUpdateFeeRate feeRateCmd)
        {

            if (feeRateCmd.Id == 0)
            {
                var newFeeRate = new FeeRate
                {
                    Amount = feeRateCmd.Amount,
                    BatchId = feeRateCmd.BatchId,
                    FeeTitleId = feeRateCmd.FeeTitleId,
                    GradeId = feeRateCmd.GradeId,
                    UniqueId = feeRateCmd.UniqueId,
                    Remark = feeRateCmd.Remark,
                };
                _context.FeeRates.Add(newFeeRate);
                await _context.SaveChangesAsync();
                return newFeeRate.Id;
            }
            else
            {
                var existing = await _context.FeeRates
                                      .FirstOrDefaultAsync(i => i.Id == feeRateCmd.Id);
                if (existing == null)
                    throw new NotFoundException("fee rate not found");

                existing.Amount = feeRateCmd.Amount;
                existing.BatchId = feeRateCmd.BatchId;
                existing.FeeTitleId = feeRateCmd.FeeTitleId;
                existing.GradeId = feeRateCmd.GradeId;
                existing.UniqueId = feeRateCmd.UniqueId;
                existing.Remark = feeRateCmd.Remark;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<FeeRateDto>> FeeRateList()
        {
            return await _context.FeeRates
                .AsNoTracking()
                .Select(s => new FeeRateDto
                {
                    Id = s.Id,
                    Amount = s.Amount,
                    BatchId = s.BatchId,
                    FeeTitleId = s.FeeTitleId,
                    GradeId = s.GradeId,
                    UniqueId = s.UniqueId,
                    Remark = s.Remark,
                }).ToListAsync();
        }

        public async Task<FeeRateDto> GetFeeRateById(int id)
        {
            var feeRate = await _context.FeeRates
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (feeRate == null)
                throw new NotFoundException("feeRate not found");

            return new FeeRateDto
            {
                Id = feeRate.Id,
                Amount = feeRate.Amount,
                BatchId = feeRate.BatchId,
                FeeTitleId = feeRate.FeeTitleId,
                GradeId = feeRate.GradeId,
                UniqueId = feeRate.UniqueId,
                Remark = feeRate.Remark,
            };
        }

        public async Task<bool> DeleteFeeRate(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var feeRate = await _context.FeeRates.FindAsync(id);
            if (feeRate == null)
                throw new BadRequestException("Invalid feeRate.");

            feeRate.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region student fee.

        public async Task<int> InsertUpdateStudentFee(InsertUpdateStudentFee feeCmd)
        {

            if (feeCmd.Id == 0)
            {
                var newStudentFee = new StudentFee
                {
                    BatchId = feeCmd.BatchId,
                    FeeRateId = feeCmd.FeeRateId,
                    GradeId = feeCmd.GradeId,
                    StudentId = feeCmd.StudentId,
                    Remark = feeCmd.Remark,
                };
                _context.StudentFees.Add(newStudentFee);
                await _context.SaveChangesAsync();
                return newStudentFee.Id;
            }
            else
            {
                var existing = await _context.StudentFees
                                      .FirstOrDefaultAsync(i => i.Id == feeCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student fee not found");

                existing.BatchId = feeCmd.BatchId;
                existing.FeeRateId = feeCmd.FeeRateId;
                existing.GradeId = feeCmd.GradeId;
                existing.StudentId = feeCmd.StudentId;
                existing.Remark = feeCmd.Remark;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<StudentFeeDto>> StudentFeeList()
        {
            return await _context.StudentFees
                .AsNoTracking()
                .Select(s => new StudentFeeDto
                {
                    Id = s.Id,
                    StudentId = s.StudentId,
                    BatchId = s.BatchId,
                    FeeRateId = s.FeeRateId,
                    GradeId = s.GradeId,
                    Remark = s.Remark,
                }).ToListAsync();
        }

        public async Task<StudentFeeDto> GetStudentFeeById(int id)
        {
            var studentFee = await _context.StudentFees
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (studentFee == null)
                throw new NotFoundException("studentFee not found");

            return new StudentFeeDto
            {
                Id = studentFee.Id,
                StudentId = studentFee.StudentId,
                BatchId = studentFee.BatchId,
                FeeRateId = studentFee.FeeRateId,
                GradeId = studentFee.GradeId,
                Remark = studentFee.Remark,
            };
        }

        public async Task<bool> DeleteStudentFee(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var studentFee = await _context.StudentFees.FindAsync(id);
            if (studentFee == null)
                throw new BadRequestException("Invalid studentFee.");

            studentFee.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Discount.

        public async Task<int> InsertUpdateDiscount(InsertUpdateDiscount discountCmd)
        {

            if (discountCmd.Id == 0)
            {
                var newDiscount = new Discount
                {
                    Name = discountCmd.Name,
                    Percentage = discountCmd.Percentage,
                    SortOrder = discountCmd.SortOrder,
                    IsActive = discountCmd.IsActive,
                    Remark = discountCmd.Remark,
                };
                _context.Discounts.Add(newDiscount);
                await _context.SaveChangesAsync();
                return newDiscount.Id;
            }
            else
            {
                var existing = await _context.Discounts
                                      .FirstOrDefaultAsync(i => i.Id == discountCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student fee not found");

                existing.Name = discountCmd.Name;
                existing.Percentage = discountCmd.Percentage;
                existing.SortOrder = discountCmd.SortOrder;
                existing.IsActive = discountCmd.IsActive;
                existing.Remark = discountCmd.Remark;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<DiscountDto>> DiscountList()
        {
            return await _context.Discounts
                .AsNoTracking()
                .Select(s => new DiscountDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Percentage = s.Percentage,
                    IsActive = s.IsActive,
                    Remark = s.Remark,
                }).ToListAsync();
        }

        public async Task<DiscountDto> DiscountById(int id)
        {
            var discount = await _context.Discounts
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (discount == null)
                throw new NotFoundException("discount not found");

            return new DiscountDto
            {
                Id = discount.Id,
                Name = discount.Name,
                Percentage = discount.Percentage,
                IsActive = discount.IsActive,
                Remark = discount.Remark,
            };
        }

        public async Task<bool> DeleteDiscount(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
                throw new BadRequestException("Invalid discount.");

            discount.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Opening Balance.

        public async Task<int> InsertUpdateOpeningBalance(InsertUpdateOpeningBalance openingBalanceCmd)
        {

            if (openingBalanceCmd.Id == 0)
            {
                var newOpeningBalance = new OpeningBalance
                {
                    AdvanceAmount = openingBalanceCmd.AdvanceAmount,
                    DueAmount = openingBalanceCmd.DueAmount,
                    OpeningDate = openingBalanceCmd.OpeningDate,
                    ClosingDate = openingBalanceCmd.ClosingDate,
                    Remark = openingBalanceCmd.Remark,
                    BatchId = openingBalanceCmd.BatchId,
                    StudentId = openingBalanceCmd.StudentId,
                    GradeId = openingBalanceCmd.GradeId,
                };
                _context.OpeningBalances.Add(newOpeningBalance);
                await _context.SaveChangesAsync();
                return newOpeningBalance.Id;
            }
            else
            {
                var existing = await _context.OpeningBalances
                                      .FirstOrDefaultAsync(i => i.Id == openingBalanceCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student fee not found");

                existing.AdvanceAmount = openingBalanceCmd.AdvanceAmount;
                existing.DueAmount = openingBalanceCmd.DueAmount;
                existing.OpeningDate = openingBalanceCmd.OpeningDate;
                existing.ClosingDate = openingBalanceCmd.ClosingDate;
                existing.Remark = openingBalanceCmd.Remark;
                existing.BatchId = openingBalanceCmd.BatchId;
                existing.StudentId = openingBalanceCmd.StudentId;
                existing.GradeId = openingBalanceCmd.GradeId;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<OpeningBalanceDto>> OpeningBalanceList()
        {
            return await _context.OpeningBalances
                .AsNoTracking()
                .Select(s => new OpeningBalanceDto
                {
                    Id = s.Id,
                    AdvanceAmount = s.AdvanceAmount,
                    DueAmount = s.DueAmount,
                    OpeningDate = s.OpeningDate,
                    ClosingDate = s.ClosingDate,
                    GradeId = s.GradeId,
                    BatchId = s.BatchId,
                    StudentId = s.StudentId,
                    Remark = s.Remark,
                }).ToListAsync();
        }

        public async Task<OpeningBalanceDto> OpeningBalanceById(int id)
        {
            var openingBalance = await _context.OpeningBalances
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (openingBalance == null)
                throw new NotFoundException("opening Balance not found");

            return new OpeningBalanceDto
            {
                Id = openingBalance.Id,
                AdvanceAmount = openingBalance.AdvanceAmount,
                DueAmount = openingBalance.DueAmount,
                OpeningDate = openingBalance.OpeningDate,
                ClosingDate = openingBalance.ClosingDate,
                GradeId = openingBalance.GradeId,
                BatchId = openingBalance.BatchId,
                StudentId = openingBalance.StudentId,
                Remark = openingBalance.Remark,
            };
        }

        public async Task<bool> DeleteOpeninigBalance(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var openingBalance = await _context.OpeningBalances.FindAsync(id);
            if (openingBalance == null)
                throw new BadRequestException("Invalid opening Balance.");

            openingBalance.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion
    }
}
