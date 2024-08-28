using System.ComponentModel.DataAnnotations;

namespace Memory.DTOs.cardDtos;

public record class UpdateCardDto
(
    [Required] Guid GameId,
    [Required] Guid CardId
);
