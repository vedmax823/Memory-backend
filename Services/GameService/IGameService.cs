using Memory.Entities;
using Memory.DTOs;

namespace Memory.Services.GameService;



public interface IGameService
{
    Task<List<Game>> GetOpenGames();
    Task<Game?> GetGameById(Guid id);
    Task<Game> CreateGame(CreateGameDto gameDto, Guid userId);
    Task<Game> CheckCards(Guid gameId, Guid firstId, Guid secondId, Guid userId);
    Task<Game> JoinGame(Guid gameId, Guid userId);

}
