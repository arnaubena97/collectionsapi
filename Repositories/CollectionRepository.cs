using collectionsapi.Data;
using collectionsapi.Data.Entities;
using collectionsapi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CollectionRepository : ICollectionRepository
{
    private readonly CollectionsapiDbContext _dbContext;

    public CollectionRepository(CollectionsapiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Collection> GetByIdAsync(int id)
    {
        return await _dbContext.Collections.FindAsync(id);
    }

    public async Task<List<Collection>> GetAllAsync()
    {
        return await _dbContext.Collections.ToListAsync();
    }

    public async Task<List<Collection>> GetAllByUserIdAsync(int userId)
    {
        return await _dbContext.Collections.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task AddAsync(Collection collection)
    {
        _dbContext.Collections.Add(collection);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Collection collection)
    {
        _dbContext.Entry(collection).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var collection = await _dbContext.Collections.FindAsync(id);
        if (collection != null)
        {
            _dbContext.Collections.Remove(collection);
            await _dbContext.SaveChangesAsync();
        }
    }
}
