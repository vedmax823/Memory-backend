using System.ComponentModel.DataAnnotations;

namespace Memory.DTOs;

public record class CheckCardsDto
(
    [Required] Guid FirstId,
    [Required] Guid SecondId
);
