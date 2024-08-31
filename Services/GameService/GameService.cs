using Memory.Entities;
using Memory.DTOs;
using Memory.Mapping;
using Memory.Repositories;

namespace Memory.Services.GameService;

public class GameService(IGameRepository repository, IUserRepository userRepository) : IGameService
{
    private readonly IGameRepository _repository = repository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<List<Game>> GetOpenGames(){
        return await _repository.GetOpenGames();
    }

    public async Task<Game?> GetGameById(Guid id){
        return await _repository.GetGameById(id);
    }

    public async Task<Game> CreateGame(CreateGameDto gameDto, Guid userId)
    {
        var user = await _userRepository.GetUser(userId) 
            ?? throw new ArgumentNullException(nameof(userId), "User Not found");
        Game game = gameDto.ToEntity(user);
        return await _repository.CreateGame(game);
    }

    public async Task<Game> CheckCards(Guid gameId, Guid firstId, Guid secondId, Guid userId){
        var game = await _repository.GetGameById(gameId)
            ?? throw  new ArgumentNullException(nameof(gameId), "There is no game.");
        if (!game.IsStarted) throw new InvalidOperationException("Game hasn't started yet");

        if (game.TurnUser != userId) throw new InvalidOperationException("It's not the user's turn.");
        var firstCard = game.Field.FirstOrDefault(card => card.Id == firstId);
        var secondCard = game.Field.FirstOrDefault(card => card.Id == secondId);

        if (firstCard is null || secondCard is null) throw new ArgumentNullException("Not two card selected");
        game.OpenCards.Clear();
        if (firstCard.IsOpen && secondCard.IsOpen && (firstCard.Link == secondCard.Link))
        {
            await _repository.UpdateGame(game);
            return game;
        }
        
        
        firstCard.IsOpen = false;
        secondCard.IsOpen = false;
        SetNextTurn(ref game, game.TurnUser);
        await _repository.UpdateGame(game);
        return game;
    }

    public async Task<Game> JoinGame(Guid gameId, Guid userId)
    {
        var game = await _repository.GetGameById(gameId)
            ?? throw  new ArgumentNullException(nameof(gameId), "There is no game.");
        if (game.IsStarted) throw new InvalidOperationException("Game has started");
        var user = await _userRepository.GetUser(userId) 
            ?? throw new ArgumentNullException(nameof(gameId), "User Not found");
        var exsistUser = game.Users.FirstOrDefault(u => u.Id == user.Id);
        if (exsistUser != null) throw new InvalidOperationException("User exsist");
        if (game.Users.Count == game.MaxPlayersCount) throw new InvalidOperationException("Game is full");
        game.Users.Add(user);
        if (game.Users.Count == game.MaxPlayersCount) game.IsStarted = true;
        await _repository.UpdateGame(game);
        return game;
    }

    private static void SetNextTurn(ref Game game, Guid currentUserId)
    {
        if (game.Users.Count > 1){
            int currentIndex = game.Users.FindIndex(user => user.Id == currentUserId);
            int nextIndex = (currentIndex + 1) % game.Users.Count;
            game.TurnUser = game.Users[nextIndex].Id;
        }
    }
}
