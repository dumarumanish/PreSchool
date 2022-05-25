using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Addresses;
using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Application.Services.Addresses.Models.Filters;
using PreSchool.Application.Services.Settings;
using PreSchool.Application.Services.Settings.Models;
using PreSchool.Application.Services.Settings.Models.Command;
using PreSchool.Application.Services.Settings.Models.Dtos;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #region Seo

 
        [HttpGet("Seo")]
        public async Task<SeoSettingModel> SeoSetting()
        {
            return await _settingService.SeoSetting();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Required data for seo Post
        /// </remarks>
        /// <returns></returns>
        [HttpGet("Seo/ViewModel")]
        public SeoViewModel SeoViewModel()
        {
            return  _settingService.SeoViewModel();
        }



        [AuthorizeUser(Permissions.ManageSeoSettings)]
        [HttpPost("Seo")]
        public async Task<bool> SeoSetting(SeoSettingModel model)
        {
            return await _settingService.UpdateSeoSetting(model);
        }

        #endregion

        #region Banks

        [AuthorizeUser(Permissions.ManageBanks)]
        [HttpPost("Banks")]
        public async Task<int> InsertUpdateBank(InsertUpdateBank Banks)
        {
            if (Banks == null)
                throw new BadRequestException("Banks  is required.");
            Banks.Id = 0;
            return await _settingService.InsertUpdateBank(Banks);
        }

        [AuthorizeUser(Permissions.ManageBanks)]
        [HttpPut("Banks/{id}")]
        public async Task<int> InsertUpdateBank(int id, InsertUpdateBank Banks)
        {
            if (Banks == null)
                throw new BadRequestException("Banks is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != Banks.Id)
                throw new BadRequestException("Id doesnot match");
            return await _settingService.InsertUpdateBank(Banks);

        }

        [HttpGet("Banks")]
        public async Task<List<BankDto>> GetBanks()
        {
            return await _settingService.GetBanks();

        }

        [HttpGet("Banks/{id}")]
        public async Task<BankDto> GetBanks(int id)
        {
            return await _settingService.GetBanks(id);

        }

        [AuthorizeUser(Permissions.ManageBanks)]
        [HttpDelete("Banks/{id}")]
        public async Task<bool> DeleteBank(int id)
        {
            return await _settingService.DeleteBank(id);

        }
        #endregion


    }
}