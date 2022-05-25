using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Stores;
using PreSchool.Application.Services.Stores.Models.Commands;
using PreSchool.Application.Services.Stores.Models.Dtos;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {

        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        #region Store

        [HttpPost]
       // [AuthorizeUser(Permissions.AddStores)]
        public async Task<int> InsertStore(InsertUpdateStore Store)
        {
            if (Store == null)
                throw new BadRequestException(" Store is required.");
            Store.Id = 0;
            return await _storeService.InsertUpdateStore(Store);
        }

        [HttpPut("{id}")]
       // [AuthorizeUser(Permissions.UpdateStores)]
        public async Task<int> UpdateStore(int id, InsertUpdateStore Store)
        {
            if (Store == null)
                throw new BadRequestException("Store is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != Store.Id)
                throw new BadRequestException("Id doesnot match");
            return await _storeService.InsertUpdateStore(Store);

        }

        [HttpGet]
        public async Task<List<StoreListDto>> GetStoreList()
        {
            return await _storeService.GetStoreList();

        }

        [HttpGet("{id}")]
        public async Task<StoreDto> GetStore(int id)
        {
            return await _storeService.GetStore(id);

        }

        [HttpDelete("{id}")]
      //  [AuthorizeUser(Permissions.DeleteStores)]
        public async Task<bool> DeleteStore(int id)
        {
            return await _storeService.DeleteStore(id);

        }
        #endregion


        #region Store Images

        #region Store Image Type

        [HttpPut("Images/Types/{id}")]
       // [AuthorizeUser(Permissions.ManageStoreImageTypes)]
        public async Task<bool> UpdateStoreImageType(int id, UpdateStoreImageType type)
        {

            if (id != type.Id)
                throw new BadRequestException("Invalid  id,  id doesnot match");

            if (id == 0)
                throw new BadRequestException("Invalid  id,  id cannot be 0");

            return await _storeService.UpdateStoreImageType(type);
        }

        [HttpGet("Images/Types")]
        public async Task<List<StoreImageTypeDto>> StoreImageTypes()
        {

            return await _storeService.StoreImageTypes();
        }

        [HttpGet("Images/Types/{id}")]
        public async Task<StoreImageTypeDto> StoreImageTypes(int id)
        {
            return await _storeService.StoreImageTypes(id);
        }

        #endregion


        [HttpPost("{storeId}/Images")]
      //  [AuthorizeUser(Permissions.AddStoreImages, Permissions.UpdateStoreImages)]
        public async Task<bool> InsertImage(int storeId, [FromForm] InsertUpdateStoreImage insertImage)
        {
            if (insertImage == null)
                throw new BadRequestException("Image is required");

            if (storeId != insertImage.storeId)
                throw new BadRequestException("Invalid store id, Store id doesnot match");

            insertImage.id = 0;

            return await _storeService.InsertUpdateStoreImage(insertImage);
        }

        [HttpPut("{storeId}/Images/{id}")]
       // [AuthorizeUser(Permissions.UpdateStoreImages)]
        public async Task<bool> UpdateImage(int storeId, int id, [FromForm] InsertUpdateStoreImage insertImage)
        {
            if (insertImage == null)
                throw new BadRequestException("Image is required");

            if (storeId != insertImage.storeId)
                throw new BadRequestException("Invalid store id, Store id doesnot match");

            if (id != insertImage.id)
                throw new BadRequestException("Invalid  id,  id doesnot match");

            if (id == 0)
                throw new BadRequestException("Invalid  id,  id cannot be 0");

            return await _storeService.InsertUpdateStoreImage(insertImage);
        }

        [HttpGet("{storeId}/Images")]
        public async Task<List<StoreImageDto>> StoreImages(int storeId)
        {

            return await _storeService.StoreImages(storeId);
        }

        [HttpGet("{storeId}/Images/{id}")]
        public async Task<StoreImageDto> StoreImages(int storeId, int id)
        {
            return await _storeService.StoreImages(storeId, id);
        }

        [HttpGet("{storeId}/Images/Types{id}")]
        public async Task<List<StoreImageDto>> StoreImagesByImageType(int storeId, int id)
        {
            return await _storeService.StoreImagesByImageType(storeId, id);
        }

        [HttpDelete("{storeId}/Images/{id}")]
      //  [AuthorizeUser(Permissions.DeleteStoreImages)]
        public async Task<bool> DeleteStoreImage(int storeId, int id)
        {
            return await _storeService.DeleteStoreImage(storeId, id);
        }

        #endregion

        #region Social Medias
        [HttpPost("{storeId}/SocialMedias")]
       // [AuthorizeUser(Permissions.ManageSocialMedias)]
        public async Task<bool> InsertSocialMedia(int storeId, [FromForm] InsertUpdateStoreSocialMedia insertSocialMedia)
        {
            if (insertSocialMedia == null)
                throw new BadRequestException("SocialMedia is required");

            if (storeId != insertSocialMedia.StoreId)
                throw new BadRequestException("Invalid store id, Store id doesnot match");

            insertSocialMedia.Id = 0;

            return await _storeService.InsertUpdateStoreSocialMedia(insertSocialMedia);
        }

        [HttpPut("{storeId}/SocialMedias/{id}")]
      //  [AuthorizeUser(Permissions.ManageSocialMedias)]
        public async Task<bool> UpdateSocialMedia(int storeId, int id, [FromForm] InsertUpdateStoreSocialMedia insertSocialMedia)
        {
            if (insertSocialMedia == null)
                throw new BadRequestException("SocialMedia is required");

            if (storeId != insertSocialMedia.StoreId)
                throw new BadRequestException("Invalid store id, Store id doesnot match");

            if (id != insertSocialMedia.Id)
                throw new BadRequestException("Invalid  id,  id doesnot match");

            if (id == 0)
                throw new BadRequestException("Invalid  id,  id cannot be 0");

            return await _storeService.InsertUpdateStoreSocialMedia(insertSocialMedia);
        }

        [HttpGet("{storeId}/SocialMedias")]
        public async Task<List<StoreSocialMediaDto>> StoreSocialMedias(int storeId)
        {

            return await _storeService.StoreSocialMedias(storeId);
        }

        [HttpGet("{storeId}/SocialMedias/{id}")]
        public async Task<StoreSocialMediaDto> StoreSocialMedias(int storeId, int id)
        {
            return await _storeService.StoreSocialMedias(storeId, id);
        }

     
        [HttpDelete("{storeId}/SocialMedias/{id}")]
      //  [AuthorizeUser(Permissions.ManageSocialMedias)]
        public async Task<bool> DeleteStoreSocialMedias(int storeId, int id)
        {
            return await _storeService.DeleteStoreSocialMedia(storeId, id);
        }

        #endregion


        #region Social Medias
        [HttpPost("{storeId}/CollectionCenters")]
       // [AuthorizeUser(Permissions.ManageCollectionCenters)]
        public async Task<bool> InsertCollectionCenter(int storeId, InsertUpdateStoreCollectionCenter insertCollectionCenter)
        {
            if (insertCollectionCenter == null)
                throw new BadRequestException("Collection Center is required");

            if (storeId != insertCollectionCenter.StoreId)
                throw new BadRequestException("Invalid store id, Store id doesnot match");

            insertCollectionCenter.Id = 0;

            return await _storeService.InsertUpdateStoreCollectionCenter(insertCollectionCenter);
        }

        [HttpPut("{storeId}/CollectionCenters/{id}")]
      //  [AuthorizeUser(Permissions.ManageCollectionCenters)]
        public async Task<bool> UpdateCollectionCenter(int storeId, int id, InsertUpdateStoreCollectionCenter insertCollectionCenter)
        {
            if (insertCollectionCenter == null)
                throw new BadRequestException("Collection Center is required");

            if (storeId != insertCollectionCenter.StoreId)
                throw new BadRequestException("Invalid store id, Store id doesnot match");

            if (id != insertCollectionCenter.Id)
                throw new BadRequestException("Invalid  id,  id doesnot match");

            if (id == 0)
                throw new BadRequestException("Invalid  id,  id cannot be 0");

            return await _storeService.InsertUpdateStoreCollectionCenter(insertCollectionCenter);
        }

        [HttpGet("{storeId}/CollectionCenters")]
        public async Task<List<StoreCollectionCenterDto>> StoreCollectionCenters(int storeId)
        {

            return await _storeService.StoreCollectionCenters(storeId);
        }

        [HttpGet("{storeId}/CollectionCenters/{id}")]
        public async Task<StoreCollectionCenterDto> StoreCollectionCenters(int storeId, int id)
        {
            return await _storeService.StoreCollectionCenters(storeId, id);
        }


        [HttpDelete("{storeId}/CollectionCenters/{id}")]
      //  [AuthorizeUser(Permissions.ManageCollectionCenters)]
        public async Task<bool> DeleteStoreCollectionCenters(int storeId, int id)
        {
            return await _storeService.DeleteStoreCollectionCenter(storeId, id);
        }

        #endregion
    }
}
