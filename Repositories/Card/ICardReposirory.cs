using System;
using Memory.DTOs;
using Memory.DTOs.cardDtos;

namespace Memory.Entities;

public interface ICardReposirory
{
    Task<Game?> FindGame(UpdateCardDto updateCardDto);
    Task SaveCard(Card card);
}
