namespace AuthorizationLibrary.Context;

/// <summary>
/// Интерфейс контекста БД авторизации
/// </summary>
public interface IAuthorizationDatabaseContextFactory
{
    /// <summary>
    /// Создать контекст БД авторизации
    /// </summary>
    /// <returns></returns>
    AuthorizationDatabaseContext CreateDbContext();
}
