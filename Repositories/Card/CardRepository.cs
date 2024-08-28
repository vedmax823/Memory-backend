using Memory.Data;
using Memory.DTOs.cardDtos;
using Memory.Entities;
using Microsoft.EntityFrameworkCore;


public class CardRepository : ICardReposirory
{
    private readonly DataContext _context;

    public CardRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<Game?> FindGame(UpdateCardDto updateCardDto)
    {
        return await _context.Games
            .Include(g => g.Field)
            .AsNoTracking()
            .SingleOrDefaultAsync(g => g.Id == updateCardDto.GameId);
    }

    public async Task SaveCard(Card card)
    {
        _context.Card.Update(card);
        await _context.SaveChangesAsync();
    }
}
