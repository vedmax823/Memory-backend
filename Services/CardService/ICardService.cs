using System;
using Memory.DTOs;
using Memory.DTOs.cardDtos;
using Memory.Entities;

namespace Memory.Services.CardService;

public interface ICardService
{
    Task<Card?> GetCard(UpdateCardDto updateCardDto);
    Task<Game> UpdateCard(UpdateCardDto updateCardDto, Guid userId);
}
