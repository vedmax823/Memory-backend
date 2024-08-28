using System;
using Memory.Entities;

namespace Memory.Repositories;

public interface IUserRepository
{
    Task<User?> GetUser(Guid Id);
}
