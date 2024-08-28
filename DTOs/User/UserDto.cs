using System.ComponentModel.DataAnnotations;

namespace Memory.DTOs.User;

public record class UserDto(
    Guid Id,
    [Required] string Name,
    int UserNumber
);
