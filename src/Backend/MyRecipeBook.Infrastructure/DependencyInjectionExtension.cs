using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DatabaseAccess;
using MyRecipeBook.Infrastructure.DatabaseAccess.Repositories;
using MyRecipeBook.Infrastructure.Extensions;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseType = configuration.DatabaseType();
        if (databaseType == DatabaseType.Postgres)
            AddDbContext_Postgres(services, configuration);
        else if (databaseType == DatabaseType.MySql)
            AddDbContext_MySql(services, configuration);
        
        AddRepositories(services);
    }
    
    public static void AddDbContext_Postgres(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions => dbContextOptions.UseNpgsql(connectionString));
    }
    
    public static void AddDbContext_MySql(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        var serverVersion = new MySqlServerVersion(new Version(8, 4, 3));
        services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUnitWork, UnitOfWork>();
    }
}