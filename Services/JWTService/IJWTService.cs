using System;
using Memory.DTOs.User;

namespace Memory.Services.JWTService;

public interface IJWTService
{
    string GenerateToken(UserDto userDto);
}
