namespace AuthorizationLibrary.Models.AuthorizationProviders;

/// <summary>
/// Авторизация с помощью пароля
/// </summary>
public class PasswordAuthorizationProvider : AuthorizationProvider
{
    /// <summary>
    /// Хеш пароля
    /// </summary>
    public required string PasswordHash { get; set; }
    
    /// <summary>
    /// Дата последнего обновления пароля
    /// </summary>
    public DateTime? LastModifiedTime { get; set; }
}
