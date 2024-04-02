using collectionsapi.Data.Entities;
using collectionsapi.Data;
using collectionsapi.Repositories;
using Microsoft.EntityFrameworkCore;
namespace collectionsapi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CollectionsapiDbContext _dbContext;

        public UserRepository(CollectionsapiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> AddUserAsync(User newUser)
        {
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
            return newUser;
        }
    }
}
