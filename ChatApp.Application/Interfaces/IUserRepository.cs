using ChatApp.Application.Dtos;
using ChatApp.Domain.Entities;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Application.Interfaces;

public interface IUserRepository
{
    Task Create(CreateUser user);
    Task<User?> FindOneByUsername(NonEmptyString username);

    Task<User?> FindOneById(MongoId id);
    Task<(List<UserResDto>, NonNegativeNumber TotalCount)> Pagination(PositiveNumber pageNumber, PositiveNumber pageSize);

}

