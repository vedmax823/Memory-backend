
namespace Memory.DTOs.cardDtos;

public record class CardDto
(
    Guid Id,
    int Col,
    int Row,
    bool IsOpen,
    string Link 
);
