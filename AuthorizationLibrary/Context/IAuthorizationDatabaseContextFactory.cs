namespace AuthorizationLibrary.Context;

public interface IAuthorizationDatabaseContextFactory
{
    AuthorizationDatabaseContext CreateDbContext();
}
