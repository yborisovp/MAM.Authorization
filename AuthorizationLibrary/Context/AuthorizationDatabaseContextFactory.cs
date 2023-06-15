using Microsoft.EntityFrameworkCore;

namespace AuthorizationLibrary.Context;

/// <summary>
/// Контекст БД авторизации 
/// </summary>
public class AuthorizationDatabaseContextFactory : IAuthorizationDatabaseContextFactory
{
    private Func<string> ConnectionStringProvider { get; }

    /// <summary>
    /// Конструктор контекста
    /// </summary>
    /// <param name="connectionString"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public AuthorizationDatabaseContextFactory(string connectionString)
    {
        if (connectionString == null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        ConnectionStringProvider = () => connectionString;
    }
    
    /// <summary>
    /// Создать контекст БД авторизации
    /// </summary>
    /// <returns></returns>
    public AuthorizationDatabaseContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AuthorizationDatabaseContext>();
        optionsBuilder.UseNpgsql($"{ConnectionStringProvider()}",
            x => x.MigrationsHistoryTable(
                AuthorizationDatabaseContext.DefaultMigrationHistoryTableName,
                AuthorizationDatabaseContext.AuthorizationSchema
            ));
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSnakeCaseNamingConvention();
        return new AuthorizationDatabaseContext(optionsBuilder.Options);
    }
}
