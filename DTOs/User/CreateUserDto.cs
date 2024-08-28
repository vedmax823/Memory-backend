using System.ComponentModel.DataAnnotations;

namespace Memory.DTOs.User;

public record class CreateUserDto(
    [Required] string Name
);
