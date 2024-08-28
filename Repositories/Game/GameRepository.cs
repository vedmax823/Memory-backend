using System;
using Memory.Data;
using Memory.Entities;
using Memory.DTOs;
using Memory.Mapping;
using Microsoft.EntityFrameworkCore;

public class GameRepository : IGameRepository
{
    private readonly  DataContext _context;

    public GameRepository(DataContext dbContext){
        _context = dbContext;
    }

    public async Task<List<Game>> GetOpenGames(){
        return await _context.Games
                .Where(g => g.IsStarted == false)
                .ToListAsync();
    }

    public async Task<Game?> GetGameById(Guid id){
        return await _context.Games
            .Include(g => g.Field)
            .Include(g => g.Users)
            .SingleOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Game> CreateGame(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task UpdateGame(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }



}
