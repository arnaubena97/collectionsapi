using collectionsapi.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace collectionsapi.Repositories
{
    public interface ICollectionItemRepository
    {
        Task<CollectionItem> GetByIdAsync(int id);
        Task<List<CollectionItem>> GetAllByCollectionIdAsync(int collectionId);
        Task AddAsync(CollectionItem collectionItem);
        Task UpdateAsync(CollectionItem collectionItem);
        Task DeleteAsync(int id);
    }
}
