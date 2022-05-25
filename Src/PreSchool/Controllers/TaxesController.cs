using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Taxes;
using PreSchool.Application.Services.Taxes.Models;
using PreSchool.Application.Services.Taxes.Models.Commands;
using PreSchool.Application.Services.Taxes.Models.Dtos;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxesController : ControllerBase
    {
        private readonly ITaxService _settingService;

        public TaxesController(ITaxService settingService)
        {
            _settingService = settingService;
        }

        #region Seo


        [HttpGet("Settings")]
        public async Task<TaxSettingModel> TaxSettings()
        {
            return await _settingService.TaxSetting();
        }


        [AuthorizeUser(Permissions.ManageTaxSettings)]
        [HttpPost("Settings")]
        public async Task<bool> TaxSettings(TaxSettingModel model)
        {
            return await _settingService.UpdateTaxSetting(model);
        }

        #endregion

        #region Categories

        [AuthorizeUser(Permissions.AddTaxCategories)]
        [HttpPost("Categories")]
        public async Task<int> InsertUpdateTaxCategory(InsertUpdateTaxCategory Categories)
        {
            if (Categories == null)
                throw new BadRequestException("Category  is required.");
            Categories.Id = 0;
            return await _settingService.InsertUpdateTaxCategory(Categories);
        }

        [AuthorizeUser(Permissions.UpdateTaxCategories)]
        [HttpPut("Categories/{id}")]
        public async Task<int> InsertUpdateTaxCategory(int id, InsertUpdateTaxCategory Categories)
        {
            if (Categories == null)
                throw new BadRequestException("Category is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != Categories.Id)
                throw new BadRequestException("Id doesnot match");
            return await _settingService.InsertUpdateTaxCategory(Categories);

        }

        [HttpGet("Categories")]
        public async Task<List<TaxCategoryDto>> GetCategories()
        {
            return await _settingService.GetTaxCategories();

        }

        [HttpGet("Categories/{id}")]
        public async Task<TaxCategoryDto> GetCategories(int id)
        {
            return await _settingService.GetTaxCategories(id);

        }

        [AuthorizeUser(Permissions.DeleteTaxCategories)]
        [HttpDelete("Categories/{id}")]
        public async Task<bool> DeleteTaxCategory(int id)
        {
            return await _settingService.DeleteTaxCategory(id);

        }
        #endregion


    }
}