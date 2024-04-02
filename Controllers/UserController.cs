using collectionsapi.Data.Entities;
using collectionsapi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace CollectionsApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] User newUser)
    {
        try
        {
            var user = await _userService.SignUp(newUser);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginUser)
    {
        try
        {
            var token = await _userService.Login(loginUser);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
