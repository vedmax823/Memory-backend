using System;
using Memory.DTOs.User;
using Memory.Services.UserService;
using Microsoft.AspNetCore.Mvc;


namespace Memory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController (IUserService service){
        _service = service;
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto createUserDto)
    {
        var token = await _service.RegistrateUser(createUserDto);
        Response.Cookies.Append("jwtToken", token, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });
        return Ok(new { Message = "User registered", token });
    }


}
