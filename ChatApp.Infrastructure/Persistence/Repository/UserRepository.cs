// /ChatApp.Infrastructure/Persistence/MongoUserRepository.cs

using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Model;
using ChatApp.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using ChatApp.Domain.ValueObjects;


namespace ChatApp.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserModel> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<UserModel>("Users");
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
}