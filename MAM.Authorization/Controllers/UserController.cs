using System.ComponentModel.DataAnnotations;
using AuthorizationLibrary.Dtos;
using AuthorizationLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAM.Authorization.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> AuthorizeUser([Required][FromBody]LoginUserDto loginUserDto, CancellationToken ct)
    {
        var user = await _userService.LoginUserAsync(loginUserDto, ct);
        return Ok(user);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> RegisterUser([Required][FromBody]RegisterUserDto registerUserDto, CancellationToken ct)
    {
        var user = await _userService.RegisterUserAsync(registerUserDto, ct);
        return Ok(user);
    }

    [HttpPost]
    
    [Route("logout")]
    public async Task<ActionResult> LogoutUser(CancellationToken ct)
    {
        await _userService.LogoutUser(User.Claims, ct);
        return NoContent();
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserDto>> GetUserById(long id, CancellationToken ct)
    {
        return await _userService.GetUserById(id, ct);
    }
    
}
