using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.AppConfigurations.Models.Dtos;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Data.Entities.Stores;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.AppConfigurations
{
    public class AppFeatureService : IAppFeatureService
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;

        public AppFeatureService(
            IDateTime dateTime,
            ICurrentUserService currentUserService,
            IApplicationDbContext context
            )
        {
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _context = context;

        }
        public Task<List<AppFeatureDto>> GetAllAppFeatures()
        {
            // Check if the user have total access permission
            if (!_currentUserService.HavePermission(Permissions.ViewAllAppFeatures))
                throw new ForbiddenException();

            return _context.AppFeatures
                .Select(p => new AppFeatureDto
                {
                    Id = p.Id,
                    AppFeatureGroupId = p.AppFeatureGroupId,
                    GroupName = p.AppFeatureGroup.GroupName,
                    AppFeatureName = p.Name,
                    Description = p.Description,
                }).ToListAsync();

        }

        public async Task<List<AppFeatureByGroupDto>> GetAllAppFeaturesByGrouping()
        {

            // Check if the user have total access permission
            if (!_currentUserService.HavePermission(Permissions.ViewAllAppFeatures))
                throw new ForbiddenException();

            // Return appFeature list without Total Access
            var appFeaturesGroups = await _context.AppFeatureGroups
                .Include(p => p.AppFeatures)
                .ToListAsync();


            List<AppFeatureByGroupDto> pgs = new List<AppFeatureByGroupDto>();
            foreach (var pg in appFeaturesGroups)
            {

                var pgDto = new AppFeatureByGroupDto()
                {
                    GroupId = pg.Id,
                    GroupName = pg.GroupName,
                    AppFeatures = pg.AppFeatures
                    .Select(p => new AppFeatureDto
                    {
                        Id = p.Id,
                        AppFeatureGroupId = p.AppFeatureGroupId,
                        GroupName = p.AppFeatureGroup.GroupName,
                        AppFeatureName = p.Name,
                        Description = p.Description,
                    })
                .ToList()
                };

                pgs.Add(pgDto);
            }
            return pgs;
        }

        public async Task<List<string>> GetAppFeatureAsEnum()
        {
            // Check if the user have total access permission
            if (!_currentUserService.HavePermission(Permissions.ViewAllAppFeatures))
                throw new ForbiddenException();

            var appFeatures = await _context.AppFeatures
                                .AsNoTracking()
                                .OrderBy(p => p.AppFeatureGroupId)
                                .ToListAsync();

            var appFeatureAsEnumLists = new List<string>();
            foreach (var appFeature in appFeatures)
            {
                appFeatureAsEnumLists.Add(appFeature.Name + " = " + appFeature.Id + ",     // " + appFeature.Description);
            }
            return appFeatureAsEnumLists;
        }

        public async Task<bool> AddUpdateAppFeaturesForStore(List<AddUpdateStoreFeature> addUpdateStoreAppFeature)
        {
            // Check if the user have total access permission
            if (!_currentUserService.HavePermission(Permissions.ManageStoreAppFeatures))
                throw new ForbiddenException();

            if (addUpdateStoreAppFeature == null || addUpdateStoreAppFeature.Count == 0)
                throw new BadRequestException("No role and permisson are selected");

            var storeFeatures = addUpdateStoreAppFeature.Select(r => new StoreFeature
            {
                AppFeatureId = r.AppFeatureId,
                StoreId = r.StoreId,
                IsDeleted = r.IsDeleted
            });
            foreach (var appFeature in storeFeatures)
            {
                if (appFeature.StoreId == 0 || appFeature.AppFeatureId == 0)
                    throw new BadRequestException("Store or appFeature is not selected", $"StoreId :{appFeature.StoreId} and AppFeatureId: {appFeature.AppFeatureId}");


                // Check if there is role 
                if (await _context.Stores.AsNoTracking().FirstOrDefaultAsync(r => r.Id == appFeature.StoreId) == null)
                    throw new BadRequestException("Invalid Store", "Store doesnot exists.");

                // Check if there is appFeature
                if (await _context.AppFeatures.AsNoTracking().FirstOrDefaultAsync(p => p.Id == appFeature.AppFeatureId) == null)
                    throw new BadRequestException("Invalid appFeature", "AppFeature doesnot exists.");

                var existingRP = _context.StoreFeatures
                    .IgnoreQueryFilters()
                    .FirstOrDefault(rp => rp.StoreId == appFeature.StoreId && rp.AppFeatureId == appFeature.AppFeatureId);

                // New appFeature
                if (existingRP == null)
                {
                   
                    _context.StoreFeatures.Add(appFeature);


                }
                // Existing appFeature
                else
                {
                    existingRP.IsDeleted = appFeature.IsDeleted;
                }
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public Task<List<AppFeatureDto>> GetAppFeaturesOfStore(int storeId)
        {
            return (_context.StoreFeatures
                .Include(sf => sf.AppFeature)
                    .ThenInclude(p => p.AppFeatureGroup)
                .AsNoTracking()
                .Where(r => r.StoreId == storeId))
                .Select(sf => new AppFeatureDto
                {
                    Id = sf.AppFeatureId,
                    AppFeatureName = sf.AppFeature.Name,
                    Description = sf.AppFeature.Description,
                    GroupName = sf.AppFeature.AppFeatureGroup.GroupName,
                    AppFeatureGroupId = sf.AppFeature.AppFeatureGroupId
                }).ToListAsync();
        }

        public async Task<bool> HaveFeature(AppFeaturesEnum appFeature, bool isCurrentLoginStore = true, int storeId = 1)
        {

            // If the current user is superadmin then user have all the feature
            if (_currentUserService.HavePermission(Permissions.TotalAccess))
                return true;

            //if (isCurrentLoginStore)
            //    storeId = _currentUserService.StoreId;
            var features = await _context.StoreFeatures
                         .AsNoTracking()
                         .Where(r => r.StoreId == storeId)
                         .Select(sf => new
                         {
                             FeatureId = sf.AppFeatureId
                         }).ToListAsync();

            return features.Select(f => f.FeatureId).Contains((int)appFeature);

        }


    }


}
