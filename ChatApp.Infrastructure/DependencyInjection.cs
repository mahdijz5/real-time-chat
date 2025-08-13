// /ChatApp.Infrastructure/DependencyInjection.cs
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

        return services;
    }
}