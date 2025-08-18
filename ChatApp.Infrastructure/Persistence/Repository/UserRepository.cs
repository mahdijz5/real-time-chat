using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Model;
using ChatApp.Domain.Entities;
using MongoDB.Driver;
using ChatApp.Domain.ValueObjects;
using AutoMapper;
using ChatApp.Application.Dtos;


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

        return User.MkUnsafe(model._id, model.Username, model.Password, model.ChatRoomIds);
    }

    public async Task<User?> FindOneById(MongoId id)
    {
        var model = await _users.Find(u => u._id == id.Value).FirstOrDefaultAsync();

        if (model is null)
        {
            return null;
        }

        return User.MkUnsafe(model._id, model.Username, model.Password, model.ChatRoomIds);
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


    public async Task<User?> FindOneAndUpdate(User user)
    {
        var filter = Builders<UserModel>.Filter.Eq("_id", user.Id.Value);
        var chatRoomValues = user.ChatRoomIds.Select(c => c.Value);

        var update = Builders<UserModel>.Update
            .Set(u => u.Username, user.Username.Value)
            .Set(u => u.ChatRoomIds, chatRoomValues);

        var updatedUser = await _users.FindOneAndUpdateAsync(filter, update);
        return User.MkUnsafe(updatedUser._id, updatedUser.Username, updatedUser.Password, updatedUser.ChatRoomIds);
    }
}