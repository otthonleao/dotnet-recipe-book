using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;
using MySqlConnector;
using Npgsql;

namespace MyRecipeBook.Infrastructure.Migrations;

public class DatabaseMigration
{
    public static void Migrate(DatabaseType databaseType, string connectionString, IServiceProvider serviceProvider)
    {
        if (databaseType == DatabaseType.Postgres)
            EnsureDatabaseCreated_Postgres(connectionString);
        else if (databaseType == DatabaseType.MySql)
            EnsureDatabaseCreated_MySql(connectionString);
        
        MigrationDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated_Postgres(string connectionString)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.Database;
        connectionStringBuilder.Remove("Database");
        
        var parameters = new DynamicParameters();
        parameters.Add("database", databaseName);
        
        using var dbConnection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
        // var records = dbConnection.Query("SELECT schema_name FROM information_schema.schemata WHERE schema_name = @database;", parameters);
        var records = dbConnection.Query("SELECT * FROM pg_database WHERE datname = @database", new { database = databaseName });
        
        if (!records.Any())
            dbConnection.Execute($"CREATE DATABASE \"{databaseName}\"");
        else
            Console.WriteLine($"CODE_INFO => DATABASE '{databaseName}' JÁ EXISTE.");
    }
    
    private static void EnsureDatabaseCreated_MySql(string connectionString)
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.Database;
        connectionStringBuilder.Remove("Database");
        
        var parameters = new DynamicParameters();
        parameters.Add("database", databaseName);
        
        using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);
        var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @database", parameters);

        if (!records.Any())
            dbConnection.Execute($"CREATE DATABASE `{databaseName}`");
        else
            Console.WriteLine($"CODE_INFO => DATABASE '{databaseName}' JÁ EXISTE.");
    }
    
    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}