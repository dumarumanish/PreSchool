using PreSchool.Application.Exceptions;
using PreSchool.Application.HelperClasses;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.Files;
using PreSchool.Application.Services.Stores.Models.Commands;
using PreSchool.Application.Services.Stores.Models.Dtos;
using PreSchool.Data;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Stores;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Stores
{
    public class StoreService : IStoreService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;
        private readonly IAppFeatureService _appFeatureService;

        public StoreService(
            IApplicationDbContext context,
            IDateTime dateTime,
            ICurrentUserService currentUserService,
            IFileService fileService,
            IAppFeatureService appFeatureService
            )
        {
            _context = context;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _fileService = fileService;
            _appFeatureService = appFeatureService;
        }

        #region Store

        public async Task<int> InsertUpdateStore(InsertUpdateStore store)
        {
            if (store.Id == 0)
            {
                // Check for feature
                if (!(await _appFeatureService.HaveFeature(AppFeaturesEnum.MultipleStores)))
                    throw new ForbiddenException("Forbidden", "You cannot add new store, Contact Administrator");


                //if (!_currentUserService.HavePermission(Permissions.AddStores))
                //    throw new ForbiddenException("Forbidden", "You cannot add new store, Contact Administrator");

                var newStore = new Store
                {
                    DisplayOrder = store.DisplayOrder,
                    Name = store.Name,
                    PAN = store.PAN,
                    PhoneNumber = store.PhoneNumber,
                    Url = store.Url,
                    Email = store.Email,
                };
                // Address
                if (store.Addresses != null && store.Addresses.Count > 0)
                {
                    foreach (var address in store.Addresses)
                    {
                        newStore.StoreAddresss.Add(new StoreAddress
                        {
                            DisplayOrder = address.DisplayOrder,
                            Address = new Data.Entities.Address()
                            {
                                Email = address.Address.Email,
                                CountryId = address.Address.CountryId == 0 ? null : address.Address.CountryId,
                                CountryProvinceId = address.Address.CountryProvinceId == 0 ? null : address.Address.CountryProvinceId,
                                CityId = address.Address.CityId,
                                RegionId = address.Address.RegionId,
                                Address1 = address.Address.Address1,
                                Address2 = address.Address.Address2,
                                ZipPostalCode = address.Address.ZipPostalCode,
                                PhoneNumber = address.Address.PhoneNumber,
                                SecondaryPhoneNumber = address.Address.SecondaryPhoneNumber,
                                FaxNumber = address.Address.FaxNumber,
                                MapLink = address.Address.MapLink,
                            }
                        });
                    }

                }
                _context.Stores.Add(newStore);
                await _context.SaveChangesAsync();
                return newStore.Id;
            }
            else
            {

                //if (!_currentUserService.HavePermission(Permissions.UpdateStores))
                //    throw new ForbiddenException();

                var existing = await _context.Stores
                    .Include(s => s.StoreAddresss)
                        .ThenInclude(a => a.Address)
                    .FirstOrDefaultAsync(s => s.Id == store.Id);
                if (existing == null)
                    throw new BadRequestException("Invalid store", "Invalid store Id");

                existing.DisplayOrder = store.DisplayOrder;
                existing.Name = store.Name;
                existing.PAN = store.PAN;
                existing.PhoneNumber = store.PhoneNumber;
                existing.Url = store.Url;
                existing.Email = store.Email;

                // Address
                if (store.Addresses != null && store.Addresses.Count > 0)
                {
                    foreach (var address in store.Addresses)
                    {
                        if (address.Id == 0)
                        {
                            existing.StoreAddresss.Add(new StoreAddress
                            {
                                DisplayOrder = address.DisplayOrder,
                                Address = new Data.Entities.Address()
                                {
                                    Email = address.Address.Email,
                                    CountryId = address.Address.CountryId == 0 ? null : address.Address.CountryId,
                                    CountryProvinceId = address.Address.CountryProvinceId == 0 ? null : address.Address.CountryProvinceId,
                                    CityId = address.Address.CityId,
                                    RegionId = address.Address.RegionId,
                                    Address1 = address.Address.Address1,
                                    Address2 = address.Address.Address2,
                                    ZipPostalCode = address.Address.ZipPostalCode,
                                    PhoneNumber = address.Address.PhoneNumber,
                                    SecondaryPhoneNumber = address.Address.SecondaryPhoneNumber,
                                    FaxNumber = address.Address.FaxNumber,
                                    MapLink = address.Address.MapLink,
                                }
                            });

                        }
                        else
                        {
                            var existingAddress = existing.StoreAddresss
                                .FirstOrDefault(a => a.Id == address.Id);
                            if (existingAddress == null)
                                throw new BadRequestException("Invalid existing store address");

                            existingAddress.DisplayOrder = address.DisplayOrder;
                            existingAddress.Address.Email = address.Address.Email;
                            existingAddress.Address.CountryId = address.Address.CountryId == 0 ? null : address.Address.CountryId;
                            existingAddress.Address.CountryProvinceId = address.Address.CountryProvinceId == 0 ? null : address.Address.CountryProvinceId;
                            existingAddress.Address.CityId = address.Address.CityId;
                            existingAddress.Address.RegionId = address.Address.RegionId;
                            existingAddress.Address.Address1 = address.Address.Address1;
                            existingAddress.Address.Address2 = address.Address.Address2;
                            existingAddress.Address.ZipPostalCode = address.Address.ZipPostalCode;
                            existingAddress.Address.PhoneNumber = address.Address.PhoneNumber;
                            existingAddress.Address.SecondaryPhoneNumber = address.Address.SecondaryPhoneNumber;
                            existingAddress.Address.FaxNumber = address.Address.FaxNumber;
                        }
                    }

                }

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }


        public async Task<List<StoreListDto>> GetStoreList()
        {
            return await _context.Stores
                .AsNoTracking()
                .Select(s => new StoreListDto
                {
                    Id = s.Id,
                    DisplayOrder = s.DisplayOrder,
                    Email = s.Email,
                    Name = s.Name,
                    PAN = s.PAN,
                    PhoneNumber = s.PhoneNumber,
                    Url = s.Url,
                }).ToListAsync();
        }


        public async Task<bool> DeleteStore(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
                throw new BadRequestException("Invalid store ");

            store.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<StoreDto> GetStore(int id)
        {
            var store = await _context.Stores
                .Include(n => n.StoreAddresss)
                    .ThenInclude(a => a.Address)
                        .ThenInclude(a => a.Country)
                .Include(n => n.StoreAddresss)
                    .ThenInclude(a => a.Address)
                        .ThenInclude(a => a.CountryProvince)
                .Include(n => n.StoreAddresss)
                    .ThenInclude(a => a.Address)
                        .ThenInclude(a => a.City)
                .Include(n => n.StoreAddresss)
                    .ThenInclude(a => a.Address)
                        .ThenInclude(a => a.Region)
                .Include(s => s.StoreImages)
                    .ThenInclude(s => s.File)
                .Include(s => s.StoreImages)
                    .ThenInclude(s => s.StoreImageType)
                .Include(s => s.StoreSocialMedias)
                    .ThenInclude(sm => sm.SocialMediaLogo)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (store == null)
                throw new NotFoundException("Store not found");

            return new StoreDto
            {
                Id = store.Id,
                DisplayOrder = store.DisplayOrder,
                Name = store.Name,
                PAN = store.PAN,
                PhoneNumber = store.PhoneNumber,
                Url = store.Url,
                Email = store.Email,
                StoreAddresses = store.StoreAddresss.Select(a => new StoreAddressDto
                {
                    DisplayOrder = a.DisplayOrder,
                    Id = a.Id,
                    StoreId = a.StoreId,
                    Address = new Addresses.Models.Dtos.AddressDto
                    {
                        Id = a.Address.Id,
                        Email = a.Address.Email,
                        CountryId = a.Address.CountryId,
                        CountryProvinceId = a.Address.CountryProvinceId,
                        City = a.Address.City?.Name,
                        CityId = a.Address.CityId,
                        RegionId = a.Address.RegionId,
                        Country = a.Address.Country?.Name,
                        Region = a.Address.Region?.Name,
                        CountryProvince = a.Address.CountryProvince?.Name,
                        Address1 = a.Address.Address1,
                        Address2 = a.Address.Address2,
                        ZipPostalCode = a.Address.ZipPostalCode,
                        PhoneNumber = a.Address.PhoneNumber,
                        SecondaryPhoneNumber = a.Address.SecondaryPhoneNumber,
                        FaxNumber = a.Address.FaxNumber,
                    }
                }).ToList(),
                StoreImages = store.StoreImages.Select(i => new StoreImageDto
                {
                    StoreId = i.StoreId,
                    Id = i.Id,
                    Description = i.Description,
                    Name = i.Name,
                    StoreImageType = i.StoreImageType.Name,
                    StoreImageTypeId = i.StoreImageTypeId,
                    ImageLocation = i.File.Path,
                    DisplayOrder = i.DisplayOrder,
                    Heading = i.Heading,
                    SubHeading = i.SubHeading,
                    RedirectLink = i.RedirectLink,
                    Title = i.Title,
                    IsActive = i.IsActive
                }).OrderBy(l => l.DisplayOrder).ToList(),
                StoreSocialMedias = store.StoreSocialMedias.Select(s => new StoreSocialMediaDto
                {
                    Id = s.Id,
                    SiteName = s.SiteName,
                    SocialMediaLogo = s.SocialMediaLogo?.Path,
                    SocialMediaLogoId = s.SocialMediaLogoId,
                    StoreId = s.StoreId,
                    Url = s.Url,
                    DisplayOrder = s.DisplayOrder,
                    SocialMediaLogoClass = s.SocialMediaLogoClass
                }).OrderBy(l => l.DisplayOrder).ToList()

            };
        }
        #endregion

        #region Store Images

        #region Store image types

        public async Task<List<StoreImageTypeDto>> StoreImageTypes()
        {
            return await _context.StoreImageTypes
                    .Select(t => new StoreImageTypeDto
                    {
                        AllowMultiple = t.AllowMultiple,
                        Description = t.Description,
                        DisplayName = t.DisplayName,
                        DisplayOrder = t.DisplayOrder,
                        Height = t.Height,
                        Id = t.Id,
                        Name = t.Name,
                        SizeLimitInKB = t.SizeLimitInKB,
                        Width = t.Width
                    }).OrderBy(t => t.DisplayOrder)
                    .ToListAsync();
        }

        public async Task<StoreImageTypeDto> StoreImageTypes(int id)
        {
            var imageType = await _context.StoreImageTypes
                    .Select(t => new StoreImageTypeDto
                    {
                        AllowMultiple = t.AllowMultiple,
                        Description = t.Description,
                        DisplayName = t.DisplayName,
                        DisplayOrder = t.DisplayOrder,
                        Height = t.Height,
                        Id = t.Id,
                        Name = t.Name,
                        SizeLimitInKB = t.SizeLimitInKB,
                        Width = t.Width
                    }).FirstOrDefaultAsync(i => i.Id == id);

            if (imageType == null)
                throw new NotFoundException("Store type not found");

            return imageType;
        }

        public async Task<bool> UpdateStoreImageType(UpdateStoreImageType imageType)
        {
            var type = await _context.StoreImageTypes
                            .FirstOrDefaultAsync(i => i.Id == imageType.Id);

            if (type == null)
                throw new NotFoundException("Store image type not found");

            type.AllowMultiple = imageType.AllowMultiple;
            type.DisplayOrder = imageType.DisplayOrder;
            type.Height = imageType.Height;
            type.Id = imageType.Id;
            type.SizeLimitInKB = imageType.SizeLimitInKB;
            type.Width = imageType.Width;
            return (await _context.SaveChangesAsync()) > 0;
        }

        #endregion

        public async Task<bool> InsertUpdateStoreImage(InsertUpdateStoreImage image)
        {

            if (image == null || image.image == null)
                throw new BadRequestException("No logo");

            // Check if the file is image
            if (!image.image.IsImage())
                throw new BadRequestException("Only image file is supported");

            var store = await _context.Stores
                    .Include(s => s.StoreImages)
                    .FirstOrDefaultAsync(s => s.Id == image.storeId);

            if (store == null)
                throw new BadRequestException("Invalid Store");

            var imageType = await _context.StoreImageTypes
                            .AsNoTracking()
                            .FirstOrDefaultAsync(i => i.Id == image.storeImageTypeId);

            if (imageType == null)
                throw new BadRequestException("Invalid store image type");

            // Check for the image type properties
            // Check for size limit
            if (imageType.SizeLimitInKB > 0)
            {
                if (image.image.Length > imageType.SizeLimitInKB * 1024)
                    throw new BadRequestException($"File size limit exceeded. Allowed size is {imageType.SizeLimitInKB} KB");
            }

            // Check for width height 
            if (imageType.Width > 0 || imageType.Height > 0)
            {
                using (var file = Image.FromStream(image.image.OpenReadStream()))
                {
                    if (file.Width != imageType.Width)
                        throw new BadRequestException($"Image width is invalid. Allowed image width is {imageType.Width}");

                    if (file.Height != imageType.Height)
                        throw new BadRequestException($"Image height is invalid. Allowed image height is {imageType.Height}");

                }
            }

            // Check for allow multiple, if not allowed then delete existing
            if (!imageType.AllowMultiple)
            {
                store.StoreImages.Where(i => i.StoreImageTypeId == image.storeImageTypeId)
                    .ToList()
                    .ForEach(im => im.IsDeleted = true);

            }
            var fileId = await _fileService.InsertFile(new Files.Models.InsertFileCommand { EntityName = nameof(StoreImage), File = image.image });

            if (fileId > 0)
            {
                if (image.id == 0)
                    store.StoreImages.Add(new StoreImage
                    {
                        FileId = fileId,
                        Description = image.description,
                        Name = image.name,
                        StoreImageTypeId = image.storeImageTypeId,
                        StoreId = image.storeId,
                        DisplayOrder = image.displayOrder,
                        Heading = image.heading,
                        SubHeading = image.subHeading,
                        RedirectLink = image.redirectLink,
                        Title = image.title,
                        IsActive = image.IsActive
                    });
                else
                {
                    var existing = store.StoreImages.FirstOrDefault(i => i.Id == image.id);
                    if (existing == null)
                        throw new BadRequestException("Invalid store image, Store image not found");
                    existing.FileId = fileId;
                    existing.Description = image.description;
                    existing.Name = image.name;
                    existing.StoreImageTypeId = image.storeImageTypeId;
                    existing.StoreId = image.storeId;
                    existing.DisplayOrder = image.displayOrder;
                    existing.Heading = image.heading;
                    existing.SubHeading = image.subHeading;
                    existing.RedirectLink = image.redirectLink;
                    existing.Title = image.title;
                    existing.IsActive = image.IsActive;

                }
            }

            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> DeleteStoreImage(int storeId, int id)
        {
            var storeImage = await _context.StoreImages
                .FirstOrDefaultAsync(s => s.Id == id && s.StoreId == storeId);

            if (storeImage == null)
                throw new NotFoundException("Store image not found");

            storeImage.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<StoreImageDto>> StoreImages(int storeId)
        {
            return await _context.StoreImages
                      .Include(s => s.StoreImageType)
                      .AsNoTracking()
                      .Where(n => n.StoreId == storeId)
                      .OrderBy(s => s.StoreImageTypeId)
                      .ThenBy(s => s.DisplayOrder)
                      .Select(i => new StoreImageDto
                      {
                          StoreId = i.StoreId,
                          Id = i.Id,
                          Description = i.Description,
                          Name = i.Name,
                          StoreImageType = i.StoreImageType.Name,
                          StoreImageTypeId = i.StoreImageTypeId,
                          ImageLocation = i.File.Path,
                          DisplayOrder = i.DisplayOrder,
                          Heading = i.Heading,
                          SubHeading = i.SubHeading,
                          RedirectLink = i.RedirectLink,
                          Title = i.Title,
                          IsActive = i.IsActive,
                      })
                      .ToListAsync();

        }

        public async Task<StoreImageDto> StoreImages(int storeId, int id)
        {
            var storeImage = await _context.StoreImages
                      .Include(s => s.StoreImageType)
                      .AsNoTracking()
                      .Select(i => new StoreImageDto
                      {
                          StoreId = i.StoreId,
                          Id = i.Id,
                          Description = i.Description,
                          Name = i.Name,
                          StoreImageType = i.StoreImageType.Name,
                          StoreImageTypeId = i.StoreImageTypeId,
                          ImageLocation = i.File.Path,
                          DisplayOrder = i.DisplayOrder,
                          Heading = i.Heading,
                          SubHeading = i.SubHeading,
                          RedirectLink = i.RedirectLink,
                          Title = i.Title,
                          IsActive = i.IsActive,
                      })
                      .FirstOrDefaultAsync(i => i.StoreId == storeId && i.Id == id);

            if (storeImage == null)
                throw new NotFoundException("Store image not found");

            return storeImage;
        }

        public async Task<List<StoreImageDto>> StoreImagesByImageType(int storeId, int storeImageTypeId)
        {
            return await _context.StoreImages
                      .Include(s => s.StoreImageType)
                      .AsNoTracking()
                      .Where(n => n.StoreId == storeId && n.StoreImageTypeId == storeImageTypeId)
                      .OrderBy(s => s.DisplayOrder)
                      .Select(i => new StoreImageDto
                      {
                          StoreId = i.StoreId,
                          Id = i.Id,
                          Description = i.Description,
                          Name = i.Name,
                          StoreImageType = i.StoreImageType.Name,
                          StoreImageTypeId = i.StoreImageTypeId,
                          ImageLocation = i.File.Path,
                          DisplayOrder = i.DisplayOrder,
                          Heading = i.Heading,
                          SubHeading = i.SubHeading,
                          RedirectLink = i.RedirectLink,
                          Title = i.Title,
                          IsActive = i.IsActive,
                      })
                      .ToListAsync();

        }

        #endregion

        #region Store Social Media
        public async Task<bool> InsertUpdateStoreSocialMedia(InsertUpdateStoreSocialMedia socialMedia)
        {

            var store = await _context.Stores
                   .Include(s => s.StoreSocialMedias)
                   .FirstOrDefaultAsync(s => s.Id == socialMedia.StoreId);

            if (store == null)
                throw new BadRequestException("Invalid Store");

            // Check for site logo
            int? fileId = null;
            if (socialMedia != null && socialMedia.SiteLogo != null)
            {
                // Check if the file is socialMedia
                if (!socialMedia.SiteLogo.IsImage())
                    throw new BadRequestException("Only image file is supported");

                fileId = await _fileService.InsertFile(new Files.Models.InsertFileCommand { EntityName = nameof(StoreSocialMedia), File = socialMedia.SiteLogo });
            }

            if (socialMedia.Id == 0)
                store.StoreSocialMedias.Add(new StoreSocialMedia
                {
                    StoreId = socialMedia.StoreId,
                    SiteName = socialMedia.SiteName,
                    Url = socialMedia.Url,
                    SocialMediaLogoId = fileId,
                    DisplayOrder = socialMedia.DisplayOrder,
                    SocialMediaLogoClass = socialMedia.SocialMediaLogoClass
                });
            else
            {
                var existing = store.StoreSocialMedias.FirstOrDefault(i => i.Id == socialMedia.Id);
                if (existing == null)
                    throw new BadRequestException("Invalid store socialMedia, Store socialMedia not found");
                existing.SiteName = socialMedia.SiteName;
                existing.Url = socialMedia.Url;
                existing.DisplayOrder = socialMedia.DisplayOrder;
                existing.SocialMediaLogoClass = socialMedia.SocialMediaLogoClass;

                // New site logo?
                if (socialMedia.SiteLogo != null && fileId != null)
                    existing.SocialMediaLogoId = fileId;

            }

            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> DeleteStoreSocialMedia(int storeId, int id)
        {
            var storeSocialMedia = await _context.StoreSocialMedias
                .FirstOrDefaultAsync(s => s.Id == id && s.StoreId == storeId);

            if (storeSocialMedia == null)
                throw new NotFoundException("Store social media not found");

            storeSocialMedia.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<StoreSocialMediaDto>> StoreSocialMedias(int storeId)
        {
            var storeSocialMedias = await _context.StoreSocialMedias
                      .Include(s => s.SocialMediaLogo)
                      .AsNoTracking()
                      .Where(n => n.StoreId == storeId)
                      .OrderBy(s => s.DisplayOrder)
                      .Select(i => new StoreSocialMediaDto
                      {
                          StoreId = i.StoreId,
                          Id = i.Id,
                          SiteName = i.SiteName,
                          Url = i.Url,
                          DisplayOrder = i.DisplayOrder,
                          SocialMediaLogoId = i.SocialMediaLogoId,
                          SocialMediaLogoClass = i.SocialMediaLogoClass,
                          SocialMediaLogo = i.SocialMediaLogo == null ? null : i.SocialMediaLogo.Path
                      })
                      .ToListAsync();

            if (storeSocialMedias == null)
                throw new NotFoundException("Store socialMedia not found");

            return storeSocialMedias;
        }

        public async Task<StoreSocialMediaDto> StoreSocialMedias(int storeId, int id)
        {
            var storeSocialMedia = await _context.StoreSocialMedias
                      .Include(s => s.SocialMediaLogo)
                      .AsNoTracking()
                      .Select(i => new StoreSocialMediaDto
                      {
                          StoreId = i.StoreId,
                          Id = i.Id,
                          SiteName = i.SiteName,
                          Url = i.Url,
                          DisplayOrder = i.DisplayOrder,
                          SocialMediaLogoClass = i.SocialMediaLogoClass,
                          SocialMediaLogoId = i.SocialMediaLogoId,
                          SocialMediaLogo = i.SocialMediaLogo == null ? null : i.SocialMediaLogo.Path,
                      })
                      .FirstOrDefaultAsync(i => i.StoreId == storeId && i.Id == id);

            if (storeSocialMedia == null)
                throw new NotFoundException("Store socialMedia not found");

            return storeSocialMedia;
        }

        #endregion

        #region Store Collection Center
        public async Task<bool> InsertUpdateStoreCollectionCenter(InsertUpdateStoreCollectionCenter collectionCenter)
        {

            var store = await _context.Stores
                   .Include(s => s.StoreCollectionCenters)
                   .FirstOrDefaultAsync(s => s.Id == collectionCenter.StoreId);

            if (store == null)
                throw new BadRequestException("Invalid Store");

            if (collectionCenter.Id == 0)
                store.StoreCollectionCenters.Add(new StoreCollectionCenter
                {
                    StoreId = collectionCenter.StoreId,
                    Name = collectionCenter.Name,
                    WarehouseId = collectionCenter.WarehouseId,
                    Email = collectionCenter.Email,
                    ContactPersonName = collectionCenter.ContactPersonName,
                    ContactPersonEmail = collectionCenter.ContactPersonEmail,
                    ContactPersonPhoneNumber = collectionCenter.ContactPersonPhoneNumber,
                    DisplayOrder = collectionCenter.DisplayOrder,
                    Address = new Data.Entities.Address
                    {
                        Email = collectionCenter.Address.Email,
                        CountryId = collectionCenter.Address.CountryId == 0 ? null : collectionCenter.Address.CountryId,
                        CountryProvinceId = collectionCenter.Address.CountryProvinceId == 0 ? null : collectionCenter.Address.CountryProvinceId,
                        CityId = collectionCenter.Address.CityId == 0 ? null : collectionCenter.Address.CityId,
                        RegionId = collectionCenter.Address.RegionId == 0 ? null : collectionCenter.Address.RegionId,
                        Address1 = collectionCenter.Address.Address1,
                        Address2 = collectionCenter.Address.Address2,
                        ZipPostalCode = collectionCenter.Address.ZipPostalCode,
                        PhoneNumber = collectionCenter.Address.PhoneNumber,
                        SecondaryPhoneNumber = collectionCenter.Address.SecondaryPhoneNumber,
                        FaxNumber = collectionCenter.Address.FaxNumber,
                        MapLink = collectionCenter.Address.MapLink,
                    }
                });
            else
            {
                var existing = store.StoreCollectionCenters.FirstOrDefault(i => i.Id == collectionCenter.Id);
                if (existing == null)
                    throw new BadRequestException("Invalid store collection center, Store collection center not found");
                existing.StoreId = collectionCenter.StoreId;
                existing.Name = collectionCenter.Name;
                existing.Email = collectionCenter.Email;
                existing.ContactPersonName = collectionCenter.ContactPersonName;
                existing.ContactPersonEmail = collectionCenter.ContactPersonEmail;
                existing.ContactPersonPhoneNumber = collectionCenter.ContactPersonPhoneNumber;
                existing.DisplayOrder = collectionCenter.DisplayOrder;
                existing.WarehouseId = collectionCenter.WarehouseId;
                existing.Address = new Data.Entities.Address
                {
                    Email = collectionCenter.Address.Email,
                    CountryId = collectionCenter.Address.CountryId == 0 ? null : collectionCenter.Address.CountryId,
                    CountryProvinceId = collectionCenter.Address.CountryProvinceId == 0 ? null : collectionCenter.Address.CountryProvinceId,
                    CityId = collectionCenter.Address.CityId == 0 ? null : collectionCenter.Address.CityId,
                    RegionId = collectionCenter.Address.RegionId == 0 ? null : collectionCenter.Address.RegionId,
                    Address1 = collectionCenter.Address.Address1,
                    Address2 = collectionCenter.Address.Address2,
                    ZipPostalCode = collectionCenter.Address.ZipPostalCode,
                    PhoneNumber = collectionCenter.Address.PhoneNumber,
                    SecondaryPhoneNumber = collectionCenter.Address.SecondaryPhoneNumber,
                    FaxNumber = collectionCenter.Address.FaxNumber,
                    MapLink = collectionCenter.Address.MapLink,
                };

            }

            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> DeleteStoreCollectionCenter(int storeId, int id)
        {
            var storeCollectionCenter = await _context.StoreCollectionCenters
                .FirstOrDefaultAsync(s => s.Id == id && s.StoreId == storeId);

            if (storeCollectionCenter == null)
                throw new NotFoundException("Store social media not found");

            storeCollectionCenter.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<StoreCollectionCenterDto>> StoreCollectionCenters(int storeId)
        {
            var storeCollectionCenters = await _context.StoreCollectionCenters
                        .Include(b => b.Address)
                            .ThenInclude(a => a.Country)
                        .Include(b => b.Address)
                            .ThenInclude(a => a.CountryProvince)
                        .Include(b => b.Address)
                            .ThenInclude(a => a.City)
                        .Include(b => b.Address)
                            .ThenInclude(a => a.Region)
                         .AsNoTracking()
                      .Where(n => n.StoreId == storeId)
                      .OrderBy(s => s.DisplayOrder)
                      .Select(i => new StoreCollectionCenterDto
                      {
                          Id = i.Id,
                          StoreId = i.StoreId,
                          WarehouseId = i.WarehouseId,
                          Name = i.Name,
                          Email = i.Email,
                          ContactPersonName = i.ContactPersonName,
                          ContactPersonEmail = i.ContactPersonEmail,
                          ContactPersonPhoneNumber = i.ContactPersonPhoneNumber,
                          DisplayOrder = i.DisplayOrder,
                          Address = i.Address != null ? new Addresses.Models.Dtos.AddressDto
                          {
                              Id = i.Address.Id,
                              Email = i.Address.Email,
                              CountryId = i.Address.CountryId,
                              CountryProvinceId = i.Address.CountryProvinceId,
                              City = i.Address.City == null ? null : i.Address.City.Name,
                              CityId = i.Address.CityId,
                              RegionId = i.Address.RegionId,
                              Country = i.Address.Country == null ? null : i.Address.Country.Name,
                              Region = i.Address.Region == null ? null : i.Address.Region.Name,
                              CountryProvince = i.Address.CountryProvince == null ? null : i.Address.CountryProvince.Name,
                              Address1 = i.Address.Address1,
                              Address2 = i.Address.Address2,
                              ZipPostalCode = i.Address.ZipPostalCode,
                              PhoneNumber = i.Address.PhoneNumber,
                              SecondaryPhoneNumber = i.Address.SecondaryPhoneNumber,
                              FaxNumber = i.Address.FaxNumber,
                          } : null
                      })
                      .ToListAsync();

            if (storeCollectionCenters == null)
                throw new NotFoundException("Store collectionCenter not found");

            return storeCollectionCenters;
        }

        public async Task<StoreCollectionCenterDto> StoreCollectionCenters(int storeId, int id)
        {
            var storeCollectionCenter = await _context.StoreCollectionCenters
                        .Include(b => b.Address)
                            .ThenInclude(a => a.Country)
                        .Include(b => b.Address)
                            .ThenInclude(a => a.CountryProvince)
                        .Include(b => b.Address)
                            .ThenInclude(a => a.City)
                        .Include(b => b.Address)
                            .ThenInclude(a => a.Region).AsNoTracking()
                      .FirstOrDefaultAsync(i => i.StoreId == storeId && i.Id == id);
                        
                      

            if (storeCollectionCenter == null)
                throw new NotFoundException("Store collection center not found");

            return new StoreCollectionCenterDto
            {
                Id = storeCollectionCenter.Id,
                StoreId = storeCollectionCenter.StoreId,
                Name = storeCollectionCenter.Name,
                Email = storeCollectionCenter.Email,
                WarehouseId = storeCollectionCenter.WarehouseId,
                ContactPersonName = storeCollectionCenter.ContactPersonName,
                ContactPersonEmail = storeCollectionCenter.ContactPersonEmail,
                ContactPersonPhoneNumber = storeCollectionCenter.ContactPersonPhoneNumber,
                DisplayOrder = storeCollectionCenter.DisplayOrder,
                Address = storeCollectionCenter.Address != null ? new Addresses.Models.Dtos.AddressDto
                {
                    Id = storeCollectionCenter.Address.Id,
                    Email = storeCollectionCenter.Address.Email,
                    CountryId = storeCollectionCenter.Address.CountryId,
                    CountryProvinceId = storeCollectionCenter.Address.CountryProvinceId,
                    City = storeCollectionCenter.Address.City == null ? null : storeCollectionCenter.Address.City.Name,
                    CityId = storeCollectionCenter.Address.CityId,
                    RegionId = storeCollectionCenter.Address.RegionId,
                    Country = storeCollectionCenter.Address.Country == null ? null : storeCollectionCenter.Address.Country.Name,
                    Region = storeCollectionCenter.Address.Region == null ? null : storeCollectionCenter.Address.Region.Name,
                    CountryProvince = storeCollectionCenter.Address.CountryProvince == null ? null : storeCollectionCenter.Address.CountryProvince.Name,
                    Address1 = storeCollectionCenter.Address.Address1,
                    Address2 = storeCollectionCenter.Address.Address2,
                    ZipPostalCode = storeCollectionCenter.Address.ZipPostalCode,
                    PhoneNumber = storeCollectionCenter.Address.PhoneNumber,
                    SecondaryPhoneNumber = storeCollectionCenter.Address.SecondaryPhoneNumber,
                    FaxNumber = storeCollectionCenter.Address.FaxNumber,
                } : null
            };
        }

        #endregion

        #region Store User

        //public async Task<int> InsertAppUser(InsertStoreUser storeUser)
        //{

        //    if (!_currentUserService.HavePermission(Permissions.CreateInternalUser))
        //        throw new ForbiddenException();

        //    // Remove white spaces
        //    storeUser.Username = storeUser.Username?.Trim();

        //    // Check if the username is already exists
        //    var user = await _context.AppUsers
        //        .FirstOrDefaultAsync(u => u.Username == storeUser.Username);

        //    if (user != null)
        //        throw new BadRequestException("Username is already taken");

        //    // Check for email for same app user type
        //    var email = await _context.AppUsers
        //                .FirstOrDefaultAsync(u => u.Email == storeUser.Email && u.AppUserTypeId == (int)AppUserTypeEnum.StoreUser);
        //    if (email != null)
        //        throw new BadRequestException("Email is already registered");

        //    // Check for phone number for same app user type
        //    var phoneNumber = await _context.AppUsers
        //                .FirstOrDefaultAsync(u => u.PhoneNumber == storeUser.PhoneNumber && u.AppUserTypeId == (int)AppUserTypeEnum.Internal);
        //    if (phoneNumber != null)
        //        throw new BadRequestException("Phone number is already registered");


        //    // Hash the passowrd
        //    byte[] passwordHash, passwordSalt;
        //    PasswordHelper.CreatePasswordHash(storeUser.Password, out passwordHash, out passwordSalt);



        //    // Check if the role is super admin or not
        //    if (storeUser.RoleIds.Contains((int)ReservedRoles.SuperAdmin))
        //        throw new BadRequestException("Invalid role.");

        //    // Check if the roles are valid
        //    var validRoles = await _context.Roles.Select(x => x.Id).ToListAsync();
        //    if (!storeUser.RoleIds.All(x => validRoles.Contains(x)))
        //        throw new BadRequestException("Invalid role.");

        //    var appUser = new AppUser()
        //    {
        //        Username = storeUser.Username,
        //        Active = true,
        //        FailedLoginAttempts = 0,
        //        IsDeleted = false,
        //        IsSystemAccount = false,
        //        RequireReLogin = true,
        //        AdminComment = storeUser.AdminComment,
        //        CannotLoginUntilDate = storeUser.CannotLoginUntilDate,
        //        SystemName = storeUser.Username,
        //        Email = storeUser.Email,
        //        AppUserTypeId = (int)AppUserTypeEnum.Internal,
        //        RegisteredInStoreId = storeUser.StoreId,
        //        AppUserDepartmentId = storeUser.DepartmentId,
        //        FirstName = storeUser.FirstName,
        //        LastName = storeUser.LastName,
        //        JobTitle = storeUser.JobTitle,
        //        MiddleName = storeUser.MiddleName,
        //        PhoneNumber = storeUser.PhoneNumber,
        //        AppUserRoles = storeUser.RoleIds.Select(roleId => new AppUserRole
        //        {
        //            IsDeleted = false,
        //            RoleId = roleId
        //        }).ToList(),
        //        AppUserPasswords = new List<AppUserPassword> { new AppUserPassword
        //        {
        //            CreatedOn = _dateTime.Now,
        //            EnablePasswordLifetime = storeUser.EnablePasswordLifetime,
        //            IsCurrent = true,
        //            PasswordExpiredOn = storeUser.PasswordExpiredOn,
        //            RequiredPasswordChange = storeUser.RequiredPasswordChange,
        //            PasswordHash = passwordHash,
        //            PasswordSalt = passwordSalt,
        //        } }

        //    };

        //    // Address
        //    if (storeUser.Address != null)
        //    {
        //        if (appUser.Address == null)
        //            appUser.Address = new Data.Entities.Address();

        //        appUser.Address.FirstName = storeUser.Address.FirstName;
        //        appUser.Address.LastName = storeUser.Address.LastName;
        //        appUser.Address.Email = storeUser.Address.Email;
        //        appUser.Address.Company = storeUser.Address.Company;
        //        appUser.Address.CountryId = storeUser.Address.CountryId == 0 ? null : storeUser.Address.CountryId;
        //        appUser.Address.CountryProvinceId = storeUser.Address.CountryProvinceId == 0 ? null : storeUser.Address.CountryProvinceId;
        //        appUser.Address.CityId = storeUser.Address.CityId;
        //        appUser.Address.RegionId = storeUser.Address.RegionId;
        //        appUser.Address.Address1 = storeUser.Address.Address1;
        //        appUser.Address.Address2 = storeUser.Address.Address2;
        //        appUser.Address.ZipPostalCode = storeUser.Address.ZipPostalCode;
        //        appUser.Address.PhoneNumber = storeUser.Address.PhoneNumber;
        //        appUser.Address.SecondaryPhoneNumber = storeUser.Address.SecondaryPhoneNumber;
        //        appUser.Address.FaxNumber = storeUser.Address.FaxNumber;
        //    }


        //    _context.AppUsers.Add(appUser);
        //    await _context.SaveChangesAsync();

        //    // Send welcome internal user email
        //    _emailService.WelcomeInternalUser(new WelcomeInternalUserEmailViewModel(storeUser.Username, storeUser.Email));

        //    return appUser.Id;

        //}

        #endregion
    }
}
