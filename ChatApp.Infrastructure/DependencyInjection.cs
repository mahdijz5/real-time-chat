// /ChatApp.Infrastructure/DependencyInjection.cs
using System.Reflection;
using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Persistence;
using MongoDB.Driver;

namespace ChatApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value));
        services.AddScoped<IMongoDatabase>(s => s.GetRequiredService<IMongoClient>().GetDatabase(configuration.GetSection("MongoDbSettings:DatabaseName").Value));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();


        services.AddAutoMapper(config =>
        {
            config.AddMaps(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}