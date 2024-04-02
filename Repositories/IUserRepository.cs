using collectionsapi.Data.Entities;

namespace collectionsapi.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(User newUser);
    }
}
