using System.ComponentModel.DataAnnotations;
using AuthorizationLibrary.Dtos;
using AuthorizationLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAM.Authorization.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> AuthorizeUser([Required][FromBody]LoginUserDto loginUserDto, CancellationToken ct)
    {
        var user = await _userService.LoginUserAsync(loginUserDto, ct);
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> RegisterUser([Required][FromBody]RegisterUserDto registerUserDto, CancellationToken ct)
    {
        var user = await _userService.RegisterUserAsync(registerUserDto, ct);
        return Ok(user);
    }

    [HttpPost]
    [Authorize]
    [Route("/logout")]
    public async Task<ActionResult<string>> LogoutUser()
    {
        return Ok("ТЫ АВТОРИЗОВАН!");
    }
    
}
