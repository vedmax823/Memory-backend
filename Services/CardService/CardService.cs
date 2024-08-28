using System;
using Memory.DTOs;
using Memory.DTOs.cardDtos;
using Memory.Entities;

namespace Memory.Services.CardService;

public class CardService : ICardService
{
    private readonly ICardReposirory _cardRepository;
    private readonly IGameRepository _gameRepository;

    public CardService(ICardReposirory reposirory, IGameRepository gameRepository){
        _cardRepository = reposirory;
        _gameRepository = gameRepository;
    }

    public async Task<Card?> GetCard(UpdateCardDto updateCardDto){
        var game = await _gameRepository.GetGameById(updateCardDto.GameId)
            ?? throw new ArgumentException("Game not found with the provided details.");
        var card = game.Field.FirstOrDefault(c => c is not null && c.Id == updateCardDto.CardId) 
            ?? throw new ArgumentException("Card not found with the provided details.");
        return card;
    }

    public async Task<Game> UpdateCard(UpdateCardDto updateCardDto, Guid userId){
         
        var game = await _gameRepository.GetGameById(updateCardDto.GameId) 
            ?? throw new ArgumentException("Game not found with the provided details.");
        if (!game.IsStarted) throw new InvalidOperationException("Game hasn't started yet");
        var card = game.Field.FirstOrDefault(c => c is not null && c.Id == updateCardDto.CardId) 
            ?? throw new ArgumentException("Card not found with the provided details.");

        if (game.TurnUser != userId) throw new ArgumentException("Not your turn");
        card.IsOpen = true;

        await _cardRepository.SaveCard(card);
        return game;
    } 

}
