using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Model;
using ChatApp.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using ChatApp.Domain.ValueObjects;
using AutoMapper;
using ChatApp.Application.Dtos;
using System.Text.Json;


namespace ChatApp.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserModel> _users;
    private readonly IMapper _mapper;

    public UserRepository(IMongoDatabase database, IMapper mapper)
    {
        _users = database.GetCollection<UserModel>("Users");
        _mapper = mapper;

    }

    public async Task Create(CreateUser user)
    {
        var model = new UserModel
        {
            Username = user.Username.Value,
            Password = user.Password.Value
        };
        await _users.InsertOneAsync(model);
    }

    public async Task<User?> FindOneByUsername(NonEmptyString username)
    {
        var model = await _users.Find(u => u.Username == username.Value).FirstOrDefaultAsync();

        if (model is null)
        {
            return null;
        }

        return User.MkUnsafe(model._id, model.Username, model.Password);
    }

    public async Task<User?> FindOneById(MongoId id)
    {
        var model = await _users.Find(u => u._id == id.Value).FirstOrDefaultAsync();

        if (model is null)
        {
            return null;
        }

        return User.MkUnsafe(model._id, model.Username, model.Password);
    }


    public async Task<(List<UserResDto>, NonNegativeNumber TotalCount)> Pagination(PositiveNumber pageNumber, PositiveNumber pageSize)
    {
        long totalCount = await _users.CountDocumentsAsync(Builders<UserModel>.Filter.Empty);

        var users = await _users.Find(Builders<UserModel>.Filter.Empty)
            .Skip((pageNumber.Value - 1) * pageSize.Value)
            .Limit(pageSize.Value)
            .ToListAsync();

        List<UserResDto> userList = _mapper.Map<List<UserResDto>>(users);

        return (userList, NonNegativeNumber.MkUnsafe(totalCount));
    }
}