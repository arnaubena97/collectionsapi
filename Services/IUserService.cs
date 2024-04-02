using collectionsapi.Data.Entities;

namespace collectionsapi.Services
{
    public interface IUserService
    {
        Task<User> SignUp(User newUser);
        Task<string> Login(User loginUser);
    }
}
