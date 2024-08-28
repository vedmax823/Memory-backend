using System.ComponentModel.DataAnnotations;

namespace Memory.DTOs;

public record class CreateGameDto(
    [Required] int MaxPlayersCount,
    [Required] int CardsCount
);
