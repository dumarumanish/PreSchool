using PreSchool.Application.Services.Settings.Models;
using PreSchool.Application.Services.Settings.Models.Command;
using PreSchool.Application.Services.Settings.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Settings
{
    public interface ISettingService
    {
        Task<bool> DeleteBank(int id);
        Task<List<BankDto>> GetBanks();
        Task<BankDto> GetBanks(int id);
        Task<int> InsertUpdateBank(InsertUpdateBank bank);
        Task<SeoSettingModel> SeoSetting();
        SeoViewModel SeoViewModel();
        Task<bool> UpdateSeoSetting(SeoSettingModel setting);
    }
}