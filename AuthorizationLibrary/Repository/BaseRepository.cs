using AuthorizationLibrary.Context;

namespace AuthorizationLibrary.Repository;

/// <summary>
/// 
/// </summary>
public class BaseRepository
{
    protected IAuthorizationDatabaseContextFactory ContextFactory { get; set; }

    protected BaseRepository(IAuthorizationDatabaseContextFactory contextFactory)
    {
        ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    }
}
