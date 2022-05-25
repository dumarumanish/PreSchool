using PreSchool.Application.Events;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.Files;
using PreSchool.Application.Services.Taxes.Models;
using PreSchool.Application.Services.Taxes.Models.Commands;
using PreSchool.Application.Services.Taxes.Models.Dtos;
using PreSchool.Data.Entities.Taxes;
using PreSchool.Data.Enumerations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Taxes
{
    public class TaxService : ITaxService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IFileService _fileService;
        private readonly IAppFeatureService _appFeatureService;

        public TaxService(
            IApplicationDbContext context,
            IDateTime dateTime,
            ICurrentUserService currentUserService,
            IEventPublisher eventPublisher,
            IFileService fileService,
            IAppFeatureService appFeatureService
            )
        {
            _context = context;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _eventPublisher = eventPublisher;
            _fileService = fileService;
            _appFeatureService = appFeatureService;
        }

        #region Tax Setting
        public async Task<bool> UpdateTaxSetting(TaxSettingModel setting)
        {
            // Remove previous setting
            await _context.TaxSettings.ForEachAsync(s => s.IsDeleted = true);

            var newSetting = new TaxSetting
            {
                VatPercent = setting.VatPercent,
            };
            _context.TaxSettings.Add(newSetting);

            var isSaved = (await _context.SaveChangesAsync()) > 0;
            if (isSaved)
                _eventPublisher.Publish(new TaxSettingChangedEvent(newSetting));
            return isSaved;
        }

        public async Task<TaxSettingModel> TaxSetting()
        {
            var setting = await _context.TaxSettings
                            .Select(s => new TaxSettingModel
                            {
                                VatPercent = s.VatPercent,
                            }).FirstOrDefaultAsync();

            if (setting == null)
                throw new NotFoundException("Setting not found");
            return setting;
        }
        #endregion

        #region TaxCategory

        public async Task<int> InsertUpdateTaxCategory(InsertUpdateTaxCategory taxCategory)
        {
            if (taxCategory.Id == 0)
            {
                if (!_currentUserService.HavePermission(Permissions.AddTaxCategories))
                    throw new ForbiddenException();

                // Check if taxCategory is already existed
                var existing = await _context.TaxCategories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Name == taxCategory.Name);

                if (existing != null)
                    throw new BadRequestException("Tax category is already exist.");

                var category = new TaxCategory
                {
                    Name = taxCategory.Name,
                    DisplayOrder = taxCategory.DisplayOrder,
                    Rate = taxCategory.Rate,
                };
                _context.TaxCategories.Add(category);
                await _context.SaveChangesAsync();
                return category.Id;
            }
            else
            {
                if (!_currentUserService.HavePermission(Permissions.UpdateTaxCategories))
                    throw new ForbiddenException();

                var existingCategory = await _context.TaxCategories.FindAsync(taxCategory.Id);
                if (existingCategory == null)
                    throw new BadRequestException("Invalid Tax category", "Invalid tax category Id");
                existingCategory.Name = taxCategory.Name;
                existingCategory.DisplayOrder = taxCategory.DisplayOrder;
                existingCategory.Rate = taxCategory.Rate;
                if (await _context.SaveChangesAsync() > 0)
                    return existingCategory.Id;
            }
            return 0;
        }


        public async Task<List<TaxCategoryDto>> GetTaxCategories()
        {
            return await _context.TaxCategories.Select(m => new TaxCategoryDto
            {
                Id = m.Id,
                DisplayOrder = m.DisplayOrder,
                Rate = m.Rate,
                Name = m.Name,
            })
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<TaxCategoryDto> GetTaxCategories(int id)
        {
            var taxCategory = await _context.TaxCategories
                .AsNoTracking()
                .Select(m => new TaxCategoryDto
                {
                    Id = m.Id,
                    DisplayOrder = m.DisplayOrder,
                    Rate = m.Rate,
                    Name = m.Name,
                })
                .FirstOrDefaultAsync(p => p.Id == id);
            if (taxCategory == null)
                throw new NotFoundException("Tax category not found");

            return taxCategory;
        }

        public async Task<bool> DeleteTaxCategory(int id)
        {
            if (!_currentUserService.HavePermission(Permissions.DeleteTaxCategories))
                throw new ForbiddenException();

            var taxCategory = await _context.TaxCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxCategory == null)
                throw new BadRequestException("Invalid Tax Category");

            // Check for dependency
            if (taxCategory.HasAnyRelation())
                throw new BadRequestException("Cannot delete the tax category, it is dependent by other records");

            taxCategory.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }
        #endregion
    }
}
