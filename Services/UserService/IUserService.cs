using System;
using Memory.DTOs.User;

namespace Memory.Services.UserService;

public interface IUserService
{
    Task<string> RegistrateUser(CreateUserDto createUserDto);
}
