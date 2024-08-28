using System;
using Memory.Entities;

public interface IGameRepository
{
    Task<List<Game>> GetOpenGames();
    Task<Game?> GetGameById(Guid id);
    Task<Game> CreateGame(Game game);
    Task UpdateGame(Game game);

}
