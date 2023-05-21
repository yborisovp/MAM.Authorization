using Microsoft.EntityFrameworkCore;

namespace AuthorizationLibrary.Context;

public class AuthorizationDatabaseContextFactory : IAuthorizationDatabaseContextFactory
{
    private Func<string> ConnectionStringProvider { get; }

    public AuthorizationDatabaseContextFactory(string connectionString)
    {
        if (connectionString == null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        ConnectionStringProvider = () => connectionString;
    }
    
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
