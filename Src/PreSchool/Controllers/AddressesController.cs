using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Addresses;
using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Application.Services.Addresses.Models.Filters;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        #region Countries

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns></returns>
        [HttpGet("Countries")]
        public async Task<List<CountryDto>> GetAllCountries([FromQuery] AddressFilter filter)
        {
            return await _addressService.GetCountries(filter);
        }

        [HttpGet("Countries/{id}")]
        public async Task<CountryDto> GetCountry(int id)
        {
            return await _addressService.GetCountry(id);
        }

        [HttpPost("Countries")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> InsertCountry(InsertUpdateCountry model)
        {
            model.Id = 0;
            return await _addressService.InsertCountry(model);
        }

        [HttpPut("Countries/{id}")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> UpdateCountry(int id, InsertUpdateCountry model)
        {
            if (model.Id == 0)
                throw new BadRequestException("Invalid id");

            if (model.Id != id)
                throw new BadRequestException("Id doesnot match");
            return await _addressService.InsertCountry(model);
        }

        [HttpDelete("Countries/{id}")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> DeleteCountry(int id)
        {
            return await _addressService.DeleteCountry(id);
        }


        #endregion

        #region Province
        /// <summary>
        /// Get provinces.    
        /// </summary>
        /// <param name="countryId">0 to get all provinces</param>
        /// <returns></returns>
        [HttpGet("Countries/{countryId}/Provinces")]
        public async Task<List<ProvinceDto>> ProvincesByCountryId(int countryId)
        {
            return await _addressService.GetProvinces(countryId);
        }

        [HttpGet("Countries/{countryId}/Provinces/{id}")]
        public async Task<ProvinceDto> Provinces(int countryId, int id)
        {
            return await _addressService.GetProvince(countryId,id);
        }


        [HttpPost("Countries/{countryId}/Provinces")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> InsertProvince(int countryId, InsertUpdateProvince model)
        {
            model.Id = 0;
            if (countryId != model.CountryId)
                throw new BadRequestException("CountryId doesnot match");

            return await _addressService.InsertProvince(model);
        }

        [HttpPost("Countries/{countryId}/Provinces/{id}")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> UpdateProvince(int countryId, int id, InsertUpdateProvince model)
        {
            if (countryId != model.CountryId)
                throw new BadRequestException("CountryId doesnot match");

            if (model.Id == 0)
                throw new BadRequestException("Invalid id");

            if (model.Id != id)
                throw new BadRequestException("Id doesnot match");

            return await _addressService.InsertProvince(model);
        }

        [HttpDelete("Countries/{countryId}/Provinces/{id}")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> DeleteProvince(int countryId, int id)
        {
            return await _addressService.DeleteProvince(countryId,id);
        }
        #endregion

        #region Cities

        /// <summary>
        /// Get Cities.
        /// </summary>
        /// <param name="provinceId"> 0 to get all provinces </param>
        /// <returns></returns>
        [HttpGet("Countries/Provinces/{provinceId}/Cities")]
        public async Task<IActionResult> CitiesByProvinceId(int provinceId)
        {
            return Ok(await _addressService.GetCities(provinceId));
        }

        [HttpGet("Countries/Provinces/{provinceId}/Cities/{id}")]
        public async Task<IActionResult> Cities(int provinceId, int id)
        {
            return Ok(await _addressService.GetCity(provinceId,id));
        }

        [HttpPost("Countries/Provinces/{provinceId}/Cities")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> InsertCity(int provinceId,InsertUpdateCity model)
        {
            if(model.ProvinceId == 0)
                throw new BadRequestException("Invalid id");

            if (model.ProvinceId != provinceId)
                throw new BadRequestException("Id doesnot match");

            model.Id = 0;

            return await _addressService.InsertCity(model);
        }

        [HttpPut("Countries/Provinces/{provinceId}/Cities/{id}")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> UpdateCity(int provinceId,int id, InsertUpdateCity model)
        {
            if (model.Id == 0)
                throw new BadRequestException("Invalid id");

            if (model.ProvinceId != provinceId)
                throw new BadRequestException("Province Id doesnot match");

            if(model.Id != id)
                throw new BadRequestException("Id doesnot match");

            return await _addressService.InsertCity(model);
        }

        [HttpDelete("Countries/Provinces/{provinceId}/Cities/{id}")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> DeleteCity(int provinceId,int id)
        {
            return await _addressService.DeleteCity(provinceId,id);
        }

        #endregion


        #region Regions

        /// <summary>
        /// Get regions
        /// </summary>
        /// <param name="cityId">0 to get all regions</param>
        /// <returns></returns>
        [HttpGet("Countries/Provinces/Cities/{cityId}/Regions")]
        public async Task<IActionResult> RegionsByCityId(int cityId)
        {
            return Ok(await _addressService.GetRegions(cityId));
        }

        [HttpGet("Countries/Provinces/Cities/{cityId}/Regions/{id}")]
        public async Task<IActionResult> RegionsByCityId(int cityId,int id)
        {
            return Ok(await _addressService.GetRegion(cityId,id));
        }

        [HttpPost("Countries/Provinces/Cities/{cityId}/Regions")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> InsertRegion(int cityId,InsertUpdateRegion model)
        {

            model.Id = 0;

            if (model.CityId != cityId)
                throw new BadRequestException("City Id doesnot match");

          
            return await _addressService.InsertRegion(model);
        }

        [HttpPut("Countries/Provinces/Cities/{cityId}/Regions/{id}")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> InsertRegion(int cityId, int id,InsertUpdateRegion model)
        {

            if (model.Id == 0)
                throw new BadRequestException("Invalid id");

            if (model.CityId != cityId)
                throw new BadRequestException("City Id doesnot match");

            if (model.Id != id)
                throw new BadRequestException("Id doesnot match");
            return await _addressService.InsertRegion(model);
        }

        [HttpDelete("Countries/Provinces/Cities/{cityId}/Regions/{id}")]
        [AuthorizeUser(Permissions.ManageAddresses)]
        public async Task<bool> DeleteRegion(int cityId,int id)
        {
            return await _addressService.DeleteRegion(cityId,id);
        }
        #endregion



















    }
}