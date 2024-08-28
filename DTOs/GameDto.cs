using System.Diagnostics.Eventing.Reader;
using Memory.DTOs.cardDtos;
using Memory.DTOs.User;

namespace Memory.DTOs;

public record class GameDto
(
    Guid Id,
    int Rows,
    int Cols, 
    List<CardDto> Field,
    int CardsCount,
    int MaxPlayersCount,
    List<UserDto> Users,
    Guid TurnUser,
    bool IsStarted

);
