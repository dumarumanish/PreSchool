using PreSchool.Application.Services.Taxes.Models;
using PreSchool.Application.Services.Taxes.Models.Commands;
using PreSchool.Application.Services.Taxes.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Taxes
{
    public interface ITaxService
    {
        Task<bool> DeleteTaxCategory(int id);
        Task<List<TaxCategoryDto>> GetTaxCategories();
        Task<TaxCategoryDto> GetTaxCategories(int id);
        Task<int> InsertUpdateTaxCategory(InsertUpdateTaxCategory taxCategory);
        Task<TaxSettingModel> TaxSetting();
        Task<bool> UpdateTaxSetting(TaxSettingModel setting);
    }
}