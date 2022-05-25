using PreSchool.Application.Services.AppConfigurations.Models.Dtos;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Data.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.AppConfigurations
{
    public interface IAppFeatureService
    {
        Task<bool> AddUpdateAppFeaturesForStore(List<AddUpdateStoreFeature> addUpdateStoreAppFeature);
        Task<List<AppFeatureDto>> GetAllAppFeatures();
        Task<List<AppFeatureByGroupDto>> GetAllAppFeaturesByGrouping();
        Task<List<string>> GetAppFeatureAsEnum();
        Task<List<AppFeatureDto>> GetAppFeaturesOfStore(int storeId);
        Task<bool> HaveFeature(AppFeaturesEnum appFeature, bool isCurrentLoginStore = true, int storeId = 1);
    }
}