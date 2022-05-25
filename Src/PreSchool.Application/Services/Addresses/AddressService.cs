using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Application.Services.Addresses.Models.Filters;
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Data.Entities.Common.Addresses;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Addresses
{
    public class AddressService : IAddressService
    {
        private readonly IApplicationDbContext _context;
        private readonly IAppFeatureService _featureService;
        private readonly ICurrentUserService _currentUserService;

        public AddressService(IApplicationDbContext context,
            IAppFeatureService featureService,
            ICurrentUserService currentUserService
            )
        {
            _context = context;
            _featureService = featureService;
            _currentUserService = currentUserService;
        }

        #region Countries

        public async Task<bool> InsertCountry(InsertUpdateCountry command)
        {
            var country = await _context.Countries
                            .FirstOrDefaultAsync(c => c.Id == command.Id);

            // Check if the country is already exists
            if (country == null)
            {
                country = new Country();
                _context.Countries.Add(country);
            }


            country.Name = command.Name;
            country.AllowsBilling = command.AllowsBilling;
            country.AllowsShipping = command.AllowsShipping;
            country.TwoLetterIsoCode = command.TwoLetterIsoCode;
            country.ThreeLetterIsoCode = command.ThreeLetterIsoCode;
            country.NumericIsoCode = command.NumericIsoCode;
            country.SubjectToVat = command.SubjectToVat;
            country.Published = command.Published;
            country.DisplayOrder = command.DisplayOrder;

            await _context.SaveChangesAsync();

            // For limited to store, store must have Multiple stores feature 
            if (command.LimitedToStores != null && command.LimitedToStores.LimitedToStores)
            {
                if (!await _featureService.HaveFeature(Data.Enumerations.AppFeaturesEnum.MultipleStores))
                    throw new FeatureNotAvailableException(AppFeaturesEnum.MultipleStores.ToNameString());

                country.LimitedToStores = command.LimitedToStores.LimitedToStores;

                // Add store
                foreach (var storeId in command.LimitedToStores.AddStores)
                {
                    var store = await _context.Stores
                                 .Include(s => s.StoreMappings)
                                 .FirstOrDefaultAsync(s => s.Id == storeId);
                    if (store == null)
                        throw new BadRequestException("Invalid store id in limited to store");

                    if (store.StoreMappings.FirstOrDefault(m => m.EntityId == country.Id && m.EntityName == nameof(Country)) != null)
                    {
                        store.StoreMappings.Add(new Data.Entities.Stores.StoreMapping
                        {
                            EntityId = country.Id,
                            EntityName = nameof(Country),
                        });
                    }
                }

                // Remove store
                foreach (var storeId in command.LimitedToStores.RemoveStores)
                {
                    var store = await _context.Stores
                                 .Include(s => s.StoreMappings)
                                 .FirstOrDefaultAsync(s => s.Id == storeId);
                    if (store == null)
                        throw new BadRequestException("Invalid store id in limited to store");
                    var countryStoreMappings = store.StoreMappings
                                            .Where(m => m.EntityId == country.Id && m.EntityName == nameof(Country));
                    foreach (var map in countryStoreMappings)
                    {
                        map.IsDeleted = true;
                    }
                }

                await _context.SaveChangesAsync();


            }

            return true;
        }
        public async Task<List<CountryDto>> GetCountries(AddressFilter filter)
        {
            var countries = _context.Countries
                .IgnoreDeletedNavigationProperties()
                .AsNoTracking()
                .AsQueryable();

            if (filter != null)
            {
                if (filter.AllowsBilling.HasValue)
                    countries = countries.Where(c => c.AllowsBilling == filter.AllowsBilling);

                if (filter.AllowsShipping.HasValue)
                    countries = countries.Where(c => c.AllowsShipping == filter.AllowsShipping);

                if (filter.SubjectToVat.HasValue)
                    countries = countries.Where(c => c.SubjectToVat == filter.SubjectToVat);

                if (filter.Published.HasValue)
                    countries = countries.Where(c => c.Published == filter.Published);

                if (filter.LimitedToStores.HasValue)
                    if (filter.LimitedToStores == true && _currentUserService.StoreId != null)
                    {
                        //Store mapping
                        if (await _featureService.HaveFeature(Data.Enumerations.AppFeaturesEnum.MultipleStores))
                        {
                            countries = from c in countries
                                        join sm in _context.StoreMappings
                                        on new { c1 = c.Id, c2 = nameof(Country) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                                        from sm in c_sm.DefaultIfEmpty()
                                        where !c.LimitedToStores || _currentUserService.StoreId == sm.StoreId
                                        select c;
                        }
                        else
                        {
                            countries = countries.Where(c => c.LimitedToStores == filter.LimitedToStores);
                        }
                    }
                    else
                        countries = countries.Where(c => c.LimitedToStores == filter.LimitedToStores);
            }

            return await countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name,
                AllowsBilling = c.AllowsBilling,
                AllowsShipping = c.AllowsShipping,
                TwoLetterIsoCode = c.TwoLetterIsoCode,
                ThreeLetterIsoCode = c.ThreeLetterIsoCode,
                SubjectToVat = c.SubjectToVat,
                Published = c.Published,
                DisplayOrder = c.DisplayOrder,
                NumericIsoCode = c.NumericIsoCode,
                LimitedToStores = c.LimitedToStores
            }).Distinct()
            .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
            .ToListAsync();

        }

        public async Task<CountryDto> GetCountry(int id)
        {
            var country = await _context.Countries
                .IgnoreDeletedNavigationProperties()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
                throw new NotFoundException("Country not found");

            return new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                AllowsBilling = country.AllowsBilling,
                AllowsShipping = country.AllowsShipping,
                TwoLetterIsoCode = country.TwoLetterIsoCode,
                ThreeLetterIsoCode = country.ThreeLetterIsoCode,
                SubjectToVat = country.SubjectToVat,
                Published = country.Published,
                DisplayOrder = country.DisplayOrder,
                NumericIsoCode = country.NumericIsoCode,
                LimitedToStores = country.LimitedToStores
            };

        }

        public async Task<bool> DeleteCountry(int id)
        {
            var country = await _context.Countries
                .Include(c => c.Provinces)
                .IgnoreDeletedNavigationProperties()

                .FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
                throw new NotFoundException("Not found");

            if (country.HasAnyRelation())
                throw new BadRequestException("Cannot delete the country, it is used in province");
            try
            {
                country.IsDeleted = true;
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                throw new InvalidException("Cannot delete the record. It is used by other record.");
            }

        }
        #endregion

        #region Provinces

        public async Task<bool> InsertProvince(InsertUpdateProvince command)
        {
            var country = await _context.Countries
                .Include(c => c.Provinces)
                .IgnoreDeletedNavigationProperties()

                .FirstOrDefaultAsync(p => p.Id == command.CountryId);

            if (country == null)
                throw new BadRequestException("Invalid Country", "Country not found");

            var province = country.Provinces.FirstOrDefault(p => p.Id == command.Id);
            if (province == null)
            {
                province = new Province();
                country.Provinces.Add(province);
            }

            province.Name = command.Name;
            province.Abbreviation = command.Abbreviation;
            province.DisplayOrder = command.DisplayOrder;
            province.Published = command.Published;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<ProvinceDto>> GetProvinces(int countryId = 0)
        {
            return await _context.Provinces
                .AsNoTracking()
                .Where(d => d.CountryId == countryId || (countryId == 0 && d.CountryId != 0))
                .Select(p => new ProvinceDto
                {
                    Published = p.Published,
                    DisplayOrder = p.DisplayOrder,
                    Abbreviation = p.Abbreviation,
                    Id = p.Id,
                    Name = p.Name
                })
                .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<ProvinceDto> GetProvince(int countryId,int provinceId)
        {
            var province =  await _context.Provinces
                .IgnoreDeletedNavigationProperties()

                .AsNoTracking()
                
                .FirstOrDefaultAsync(d => d.Id == provinceId && d.CountryId == countryId );

            if (province == null)
                throw new NotFoundException("Province not found");

            return new ProvinceDto
            {
                Published = province.Published,
                DisplayOrder = province.DisplayOrder,
                Abbreviation = province.Abbreviation,
                Id = province.Id,
                Name = province.Name
            };
        }
        public async Task<bool> DeleteProvince(int countryId, int id)
        {
            var province = await _context.Provinces
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(c => c.Id == id && c.CountryId == countryId);
            if (province == null)
                throw new NotFoundException("Not found");


            if (province.HasAnyRelation())
                throw new BadRequestException("Cannot delete the province, it is used in cities");
            try
            {
                province.IsDeleted = true;
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                throw new InvalidException("Cannot delete the record. It is used by other record.");
            }
        }


        #endregion

        #region Cities

        public async Task<List<CityDto>> GetCities(int provinceId = 0)
        {
            return await _context.Cities
                .IgnoreDeletedNavigationProperties()

                .AsNoTracking()
                .Where(d => d.ProvinceId == provinceId || (provinceId == 0 && d.ProvinceId != 0))
                .Select(p => new CityDto
                {
                    Published = p.Published,
                    DisplayOrder = p.DisplayOrder,
                    Id = p.Id,
                    Name = p.Name
                })
                .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<CityDto> GetCity(int provinceId, int cityId)
        {
            var city =  await _context.Cities
                .IgnoreDeletedNavigationProperties()

                .AsNoTracking()
                
                .FirstOrDefaultAsync(d => d.Id == cityId && d.ProvinceId == provinceId);

            if (city == null)
                throw new NotFoundException("City not found");

            return new CityDto
            {
                Published = city.Published,
                DisplayOrder = city.DisplayOrder,
                Id = city.Id,
                Name = city.Name
            };
        }


        public async Task<bool> InsertCity(InsertUpdateCity command)
        {
            var province = await _context.Provinces
                    .Include(c => c.Cities)
                    .FirstOrDefaultAsync(p => p.Id == command.ProvinceId);

            if (province == null)
                throw new BadRequestException("Invalid Province", "Province not found");

            var city = province.Cities.FirstOrDefault(p => p.Id == command.Id);

            if (city == null)
            {
                city = new City();
                province.Cities.Add(city);
            }

            city.Name = command.Name;
            city.DisplayOrder = command.DisplayOrder;
            city.Published = command.Published;

            return (await _context.SaveChangesAsync()) > 0;

        }


        public async Task<bool> DeleteCity(int provinceId,int id)
        {
            var city = await _context.Cities
                .Include(c => c.Regions)
                .FirstOrDefaultAsync(c => c.Id == id && c.ProvinceId != provinceId) ;
            if (city == null)
                throw new NotFoundException("Not found");

            if (city.HasAnyRelation())
                throw new BadRequestException("Cannot delete the city, it is used in regions");
            try
            {
                city.IsDeleted = true;
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                throw new InvalidException("Cannot delete the record. It is used by other record.");
            }

        }


        #endregion

        #region Regions

        public async Task<List<RegionDto>> GetRegions(int cityId = 0)
        {

            return await _context.Regions
                .IgnoreDeletedNavigationProperties()

                .AsNoTracking()
                .Where(d => d.CityId == cityId || (cityId == 0 && d.CityId != 0))
                .Select(p => new RegionDto
                {
                    Published = p.Published,
                    DisplayOrder = p.DisplayOrder,
                    Id = p.Id,
                    Name = p.Name
                })
                .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                .ToListAsync();


        }

        public async Task<RegionDto> GetRegion(int cityId, int regionId)
        {

            var region = await _context.Regions
                .IgnoreDeletedNavigationProperties()

                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == regionId && d.CityId == cityId);
               
            return new RegionDto
            {
                Published = region.Published,
                DisplayOrder = region.DisplayOrder,
                Id = region.Id,
                Name = region.Name
            };

        }
        public async Task<bool> InsertRegion(InsertUpdateRegion command)
        {
            var city = await _context.Cities
                .Include(c => c.Regions)

                .FirstOrDefaultAsync(p => p.Id == command.CityId);

            if (city == null)
                throw new BadRequestException("Invalid City", "City not found");

            var region = city.Regions.FirstOrDefault(p => p.Id == command.Id);

            if (region == null)
            {
                region = new Region();
                city.Regions.Add(region);
            }

            region.Name = command.Name;
            region.DisplayOrder = command.DisplayOrder;
            region.Published = command.Published;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteRegion(int cityId,int id)
        {
            var region = await _context.Regions
                .FirstOrDefaultAsync(c => c.Id == id && c.CityId != cityId);
            if (region == null)
                throw new NotFoundException("Not found");
            try
            {
                _context.Regions.Remove(region);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                throw new InvalidException("Cannot delete the record. It is used by other record.");
            }

        }

        #endregion


    }
}
