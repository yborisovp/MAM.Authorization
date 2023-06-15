using AuthorizationLibrary.Context;

namespace AuthorizationLibrary.Repository;

/// <summary>
/// 
/// </summary>
public class BaseRepository
{
    /// <summary>
    /// Фабрика контекста БД
    /// </summary>
    protected IAuthorizationDatabaseContextFactory ContextFactory { get; set; }

    /// <summary>
    /// Базовый класс для репозиториев
    /// </summary>
    /// <param name="contextFactory"></param>
    /// <exception cref="ArgumentNullException"></exception>
    protected BaseRepository(IAuthorizationDatabaseContextFactory contextFactory)
    {
        ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    }
}
