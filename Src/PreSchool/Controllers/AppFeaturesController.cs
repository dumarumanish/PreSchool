
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.AppConfigurations.Models.Dtos;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppFeaturesController : ControllerBase
    {

        private readonly IAppFeatureService _appFeatureService;

        public AppFeaturesController(IAppFeatureService appFeatureService)
        {
            _appFeatureService = appFeatureService;
        }


        /// <summary>
        /// Get all the appFeatures of application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeUser(Permissions.ViewAllAppFeatures)]
        public async Task<List<AppFeatureDto>> GetAllAppFeatures()
        {
            return await _appFeatureService.GetAllAppFeatures();
        }

        /// <summary>
        /// Get all the appFeatures of application by grouping
        /// </summary>
        /// <returns></returns>
        [HttpGet("AppFeatureByGrouping")]
        // [EnableQuery]
        [AuthorizeUser(Permissions.ViewAllAppFeatures)]
        public async Task<List<AppFeatureByGroupDto>> GetAllAppFeaturesByGrouping()
        {
            return await _appFeatureService.GetAllAppFeaturesByGrouping();
        }

        /// <summary>
        /// Get all the appFeatures of application as enum
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsEnum")]
        // [EnableQuery]
        [AuthorizeUser(Permissions.ViewAllAppFeatures)]
        public async Task<List<string>> GetAllAppFeaturesAsEnum()
        {
            return await _appFeatureService.GetAppFeatureAsEnum();
        }


        /// <summary>
        /// Add / Update appFeature of the store, Returns true or false
        /// </summary>
        /// <param name="storeAppFeatures"></param>
        /// <returns></returns>
        [HttpPost("Stores")]
        [AuthorizeUser(Permissions.TotalAccess)]
        public async Task<IActionResult> AddUpdateAppFeaturesForStore([FromBody] List<AddUpdateStoreFeature> storeAppFeatures)
        {
            return Ok(await _appFeatureService.AddUpdateAppFeaturesForStore(storeAppFeatures));
        }


        /// <summary>
        /// Get appFeatures of the store
        /// </summary>
        /// <param name="id">Id of store</param>
        /// <returns></returns>
        [HttpGet("Stores/{id}")]
        [AuthorizeUser(Permissions.ViewStoreAppFeatures)]
        public async Task<IActionResult> GetAppFeaturesOfStore(int id)
        {
            return Ok(await _appFeatureService.GetAppFeaturesOfStore(id));
        }
    }
}
