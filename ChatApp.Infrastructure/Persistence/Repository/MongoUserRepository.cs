// /ChatApp.Infrastructure/Persistence/MongoUserRepository.cs

using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Model;
using ChatApp.Domain.Entities;
using MongoDB.Driver;


namespace ChatApp.Infrastructure.Persistence;

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<UserModel> _users;

    public MongoUserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<UserModel>("Users");
    }

    public async Task Create(CreateUser user)
    {
        var persistenceModel = new UserModel
        {
            Username = user.Username.ToString(),
            Password = user.Password.ToString()
        };
        await _users.InsertOneAsync(persistenceModel);
    }

    public async Task<UserModel?> GetByUsernameAsync(string username)
    {
        var persistenceModel = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

        if (persistenceModel is null)
        {
            return null;
        }

        return new UserModel
        {
            Id = persistenceModel.Id,
            Username = persistenceModel.Username,
            Password = persistenceModel.Password
        };
    }
}