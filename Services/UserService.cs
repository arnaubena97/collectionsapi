using BCrypt.Net;
using collectionsapi.Services;
using collectionsapi.Data.Entities;
using collectionsapi.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace collectionsApi.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<User> SignUp(User newUser)
    {
        // check if user exists
        var existingUser = await _userRepository.GetUserByUsernameAsync(newUser.Username);
        if (existingUser != null)
        {
            throw new InvalidOperationException("This user already exists");
        }

        // encript password to store it
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);

        return await _userRepository.AddUserAsync(newUser);
    }

    public async Task<string> Login(User loginUser)
    {
        var user = await _userRepository.GetUserByUsernameAsync(loginUser.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // generate token JWT
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
            }),
            Expires = DateTime.UtcNow.AddHours(1), // expiration time token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
