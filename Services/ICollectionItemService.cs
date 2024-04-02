using collectionsapi.Data.Entities;
using System.Threading.Tasks;
namespace collectionsapi.Services
{
    public interface ICollectionItemService
    {
        Task<CollectionItem> CreateCollectionItem(CollectionItem collectionItem);
        Task<CollectionItem> GetCollectionItem(int id);
        Task<CollectionItem> UpdateCollectionItem(CollectionItem collectionItem);
        Task DeleteCollectionItem(int id);
    }
}
