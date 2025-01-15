using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static DatabaseType DatabaseType(this IConfiguration configuration)
    {
        var databaseType = configuration.GetConnectionString("DatabaseType");
        return (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType!);
    }
    
    public static string ConnectionString(this IConfiguration configuration)
    {
        var databaseType = configuration.DatabaseType();

        if (databaseType == Domain.Enums.DatabaseType.MySql)
            return configuration.GetConnectionString("ConnectionMySql")!;
        else if (databaseType == Domain.Enums.DatabaseType.Postgres)
            return configuration.GetConnectionString("ConnectionPostgres")!;
        else
            throw new Exception("Database type não conseguiu conenctar");
    }
}