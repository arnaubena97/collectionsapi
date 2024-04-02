using collectionsapi.Data.Entities;
namespace collectionsapi.Services
{
    public interface ICollectionService
    {
        Task<Collection> CreateCollection(Collection collection, string requestToken);
        Task<Collection> GetCollection(int id, string requestToken);
        Task<List<Collection>> GetAllCollections(string requestToken);
        Task<Collection> UpdateCollection(Collection collection, string requestToken);
        Task DeleteCollection(int id, string requestToken);
    }
}
