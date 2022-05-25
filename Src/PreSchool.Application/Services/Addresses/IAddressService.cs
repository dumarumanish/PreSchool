using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Application.Services.Addresses.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Addresses
{
    public interface IAddressService
    {
        Task<bool> DeleteCity(int provinceId,int id);
        Task<bool> DeleteCountry(int id);
        Task<bool> DeleteProvince(int countryId,int id);
        Task<bool> DeleteRegion(int cityId,int id);
        Task<List<CityDto>> GetCities(int provinceId = 0);
        Task<CityDto> GetCity(int provinceId,int cityId);
        Task<List<CountryDto>> GetCountries(AddressFilter filter);
        Task<CountryDto> GetCountry(int id);
        Task<ProvinceDto> GetProvince(int countryId,int provinceId);
        Task<List<ProvinceDto>> GetProvinces(int countryId = 0);
        Task<RegionDto> GetRegion(int cityId,int regionId);
        Task<List<RegionDto>> GetRegions(int cityId = 0);
        Task<bool> InsertCity(InsertUpdateCity command);
        Task<bool> InsertCountry(InsertUpdateCountry command);
        Task<bool> InsertProvince(InsertUpdateProvince command);
        Task<bool> InsertRegion(InsertUpdateRegion command);
    }
}