using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using collectionsapi.Data;
using collectionsapi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace collectionsapi.Repositories
{
    public class CollectionItemRepository : ICollectionItemRepository
    {
        private readonly CollectionsapiDbContext _dbContext;

        public CollectionItemRepository(CollectionsapiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CollectionItem collectionItem)
        {
            await _dbContext.CollectionItems.AddAsync(collectionItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CollectionItem> GetByIdAsync(int id)
        {
            return await _dbContext.CollectionItems.FindAsync(id);
        }

        public async Task UpdateAsync(CollectionItem collectionItem)
        {
            _dbContext.CollectionItems.Update(collectionItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var collectionItem = await _dbContext.CollectionItems.FindAsync(id);
            if (collectionItem != null)
            {
                _dbContext.CollectionItems.Remove(collectionItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<CollectionItem>> GetAllByCollectionIdAsync(int collectionId)
        {
            return await _dbContext.CollectionItems.Where(ci => ci.CollectionId == collectionId).ToListAsync();
        }
    }
}
