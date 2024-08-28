using System;
using Memory.Data;
using Memory.Entities;
namespace Memory.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    private readonly DataContext _context = context;

    public async Task<User?> GetUser(Guid userId){
        return await _context.Users.FindAsync(userId);
    }

}
