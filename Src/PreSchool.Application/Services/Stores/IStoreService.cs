using PreSchool.Application.Services.Stores.Models.Commands;
using PreSchool.Application.Services.Stores.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Stores
{
    public interface IStoreService
    {
        Task<bool> DeleteStore(int id);
        Task<bool> DeleteStoreImage(int storeId, int id);
        Task<bool> DeleteStoreSocialMedia(int storeId, int id);
        Task<StoreDto> GetStore(int id);
        Task<List<StoreListDto>> GetStoreList();
        Task<int> InsertUpdateStore(InsertUpdateStore store);
        Task<bool> InsertUpdateStoreImage(InsertUpdateStoreImage image);
        Task<bool> InsertUpdateStoreSocialMedia(InsertUpdateStoreSocialMedia socialMedia);
        Task<List<StoreImageDto>> StoreImages(int storeId);
        Task<StoreImageDto> StoreImages(int storeId, int id);
        Task<List<StoreImageDto>> StoreImagesByImageType(int storeId, int storeImageTypeId);
        Task<List<StoreImageTypeDto>> StoreImageTypes();
        Task<StoreImageTypeDto> StoreImageTypes(int id);
        Task<List<StoreSocialMediaDto>> StoreSocialMedias(int storeId);
        Task<StoreSocialMediaDto> StoreSocialMedias(int storeId, int id);
        Task<bool> UpdateStoreImageType(UpdateStoreImageType imageType);

        Task<bool> InsertUpdateStoreCollectionCenter(InsertUpdateStoreCollectionCenter collectionCenter);
        Task<bool> DeleteStoreCollectionCenter(int storeId, int id);
        Task<List<StoreCollectionCenterDto>> StoreCollectionCenters(int storeId);
        Task<StoreCollectionCenterDto> StoreCollectionCenters(int storeId, int id);
    }
}