namespace AuthorizationLibrary.Models;

/// <summary>
/// Модель токена обновления
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Токен
    /// </summary>
    public string Token { get; set; } = string.Empty;
    /// <summary>
    /// Время жизни токена
    /// </summary>
    public DateTimeOffset TokenExpirationDate { get; set; }
}
