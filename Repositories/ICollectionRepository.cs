using System.Collections.Generic;
using System.Threading.Tasks;
using collectionsapi.Data.Entities;
namespace collectionsapi.Repositories {
    public interface ICollectionRepository
    {
        Task<Collection> GetByIdAsync(int id);
        Task<List<Collection>> GetAllAsync();
        Task<List<Collection>> GetAllByUserIdAsync(int userId);
        Task AddAsync(Collection collection);
        Task UpdateAsync(Collection collection);
        Task DeleteAsync(int id);
    }
}