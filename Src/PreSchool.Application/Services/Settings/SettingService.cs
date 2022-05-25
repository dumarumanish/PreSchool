using PreSchool.Application.Events;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.Settings.Models;
using PreSchool.Application.Services.Settings.Models.Command;
using PreSchool.Application.Services.Settings.Models.Dtos;
using PreSchool.Data.Entities.Seo;
using PreSchool.Data.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Settings
{
    public class SettingService : ISettingService
    {

        private readonly IApplicationDbContext _context;
        private readonly IEventPublisher _eventPublisher;

        public SettingService(
            IApplicationDbContext context,
           IEventPublisher eventPublisher
            )
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        #region Seo settings

        public SeoViewModel SeoViewModel() => new SeoViewModel();
        public async Task<bool> UpdateSeoSetting(SeoSettingModel setting)
        {
            // Remove previous setting
            await _context.SeoSettings.ForEachAsync(s => s.IsDeleted = true);

            var newSetting = new Data.Entities.Seo.SeoSetting
            {
                PageTitleSeparator = setting.PageTitleSeparator,
                PageTitleSeoAdjustment = setting.PageTitleSeoAdjustmentId,
                DefaultTitle = setting.DefaultTitle,
                DefaultMetaKeywords = setting.DefaultMetaKeywords,
                DefaultMetaDescription = setting.DefaultMetaDescription,
                GenerateProductMetaDescription = setting.GenerateProductMetaDescription,
                ConvertNonWesternChars = setting.ConvertNonWesternChars,
                AllowUnicodeCharsInUrls = setting.AllowUnicodeCharsInUrls,
                CanonicalUrlsEnabled = setting.CanonicalUrlsEnabled,
                QueryStringInCanonicalUrlsEnabled = setting.QueryStringInCanonicalUrlsEnabled,
                WwwRequirement = setting.WwwRequirementId,
                TwitterMetaTags = setting.TwitterMetaTags,
                OpenGraphMetaTags = setting.OpenGraphMetaTags,
                MicrodataEnabled = setting.MicrodataEnabled,
                ReservedUrlRecordSlugs = setting.ReservedUrlRecordSlugs,
                CustomHeadTags = setting.CustomHeadTags,
            };
            _context.SeoSettings.Add(newSetting);

            var isSaved = (await _context.SaveChangesAsync()) > 0;
            if (isSaved)
                _eventPublisher.Publish(new SeoSettingChangedEvent(newSetting));
            return isSaved;
        }

        public async Task<SeoSettingModel> SeoSetting()
        {
            var setting = await _context.SeoSettings
                            .Select(s => new SeoSettingModel
                            {
                                PageTitleSeparator = s.PageTitleSeparator,
                                PageTitleSeoAdjustment = s.PageTitleSeoAdjustment.ToString(),
                                PageTitleSeoAdjustmentId = s.PageTitleSeoAdjustment,
                                DefaultTitle = s.DefaultTitle,
                                DefaultMetaKeywords = s.DefaultMetaKeywords,
                                DefaultMetaDescription = s.DefaultMetaDescription,
                                GenerateProductMetaDescription = s.GenerateProductMetaDescription,
                                ConvertNonWesternChars = s.ConvertNonWesternChars,
                                AllowUnicodeCharsInUrls = s.AllowUnicodeCharsInUrls,
                                CanonicalUrlsEnabled = s.CanonicalUrlsEnabled,
                                QueryStringInCanonicalUrlsEnabled = s.QueryStringInCanonicalUrlsEnabled,
                                WwwRequirement = s.WwwRequirement.ToString(),
                                WwwRequirementId = s.WwwRequirement,
                                TwitterMetaTags = s.TwitterMetaTags,
                                OpenGraphMetaTags = s.OpenGraphMetaTags,
                                MicrodataEnabled = s.MicrodataEnabled,
                                ReservedUrlRecordSlugs = s.ReservedUrlRecordSlugs,
                                CustomHeadTags = s.CustomHeadTags,
                            }).FirstOrDefaultAsync();

            if (setting == null)
                throw new NotFoundException("Setting not found");
            return setting;
        }
        #endregion


        #region Bank

        public async Task<int> InsertUpdateBank(InsertUpdateBank bank)
        {
            if (bank.Id == 0)
            {

                var type = new Bank
                {
                    Name = bank.Name,
                    DisplayOrder = bank.DisplayOrder,
                    IsActive = true,
                    SwiftCode = bank.SwiftCode,

                };
                _context.Banks.Add(type);
                await _context.SaveChangesAsync();
                return type.Id;
            }
            else
            {
                var existing = await _context.Banks.FindAsync(bank.Id);
                if (existing == null)
                    throw new BadRequestException("Invalid Bank", "Invalid bank Id");
                existing.Name = bank.Name;
                existing.DisplayOrder = bank.DisplayOrder;
                existing.SwiftCode = bank.SwiftCode;
                existing.IsActive = bank.IsActive;
                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }


        public async Task<List<BankDto>> GetBanks()
        {
            return await _context.Banks.Select(m => new BankDto
            {
                Id = m.Id,
                Name = m.Name,
                DisplayOrder = m.DisplayOrder,
                IsActive = m.IsActive,
                SwiftCode = m.SwiftCode
            })
                .OrderBy(m => m.DisplayOrder)
                .ToListAsync();
        }

        public async Task<BankDto> GetBanks(int id)
        {
            var bank = await _context.Banks
                .AsNoTracking()
                .Select(m => new BankDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    DisplayOrder = m.DisplayOrder,
                    IsActive = m.IsActive,
                    SwiftCode = m.SwiftCode
                })
                .OrderBy(m => m.DisplayOrder)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (bank == null)
                throw new BadRequestException("Invalid Id");

            return bank;
        }

        public async Task<bool> DeleteBank(int id)
        {
            var bank = await _context.Banks
                .FirstOrDefaultAsync(b => b.Id == id);
            if (bank == null)
                throw new BadRequestException("Invalid Bank");

            if (bank.HasAnyRelation())
                throw new InvalidOperationException("Cannot delete bank, it is used by other entity");
            bank.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }
        #endregion

    }
}
