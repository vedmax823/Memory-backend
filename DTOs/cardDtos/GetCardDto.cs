using System.ComponentModel.DataAnnotations;

namespace Memory.DTOs;

public record class GetCardDto
(
    [Required] Guid GameId,
    [Required] int Col,
    [Required] int Row
);
