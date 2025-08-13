using ChatApp.Domain.Entities;

namespace ChatApp.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
}

