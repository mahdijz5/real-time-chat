using ChatApp.Domain.Entities;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Application.Interfaces;

public interface IUserRepository
{
    Task Create(CreateUser user);
    Task<User?> FindOneByUsername(NonEmptyString username);

    Task<User?> FindOneById(MongoId id);

}

