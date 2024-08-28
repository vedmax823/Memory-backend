using System;
using Memory.Data;
using Memory.DTOs.User;
using Memory.Entities;

using Memory.Mapping;
using Memory.Services.JWTService;

namespace Memory.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IJWTService _jwtService;

    public UserService(DataContext context, IJWTService jwtService)
    {
        _jwtService = jwtService;
        _context = context;
    }

    public async Task<string> RegistrateUser(CreateUserDto createUserDto){
        var user = new User{Name = createUserDto.Name};
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return _jwtService.GenerateToken(user.ToUserdto());
        // return user.Name;
    }
}
