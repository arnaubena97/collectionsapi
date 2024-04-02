using collectionsapi.Repositories;
using collectionsapi.Data.Entities;
using System;
using System.Threading.Tasks;
namespace collectionsapi.Services
{
    public class CollectionItemService : ICollectionItemService
    {
        private readonly ICollectionItemRepository _collectionItemRepository;

        public CollectionItemService(ICollectionItemRepository collectionItemRepository)
        {
            _collectionItemRepository = collectionItemRepository;
        }

        public async Task<CollectionItem> CreateCollectionItem(CollectionItem collectionItem)
        {
            await _collectionItemRepository.AddAsync(collectionItem);
            return collectionItem;
        }

        public async Task<CollectionItem> GetCollectionItem(int id)
        {
            return await _collectionItemRepository.GetByIdAsync(id);
        }

        public async Task<CollectionItem> UpdateCollectionItem(CollectionItem collectionItem)
        {
            await _collectionItemRepository.UpdateAsync(collectionItem);
            return collectionItem;
        }

        public async Task DeleteCollectionItem(int id)
        {
            await _collectionItemRepository.DeleteAsync(id);
        }
    }
}