namespace AuthorizationLibrary.Configuration;

/// <summary>
/// Параметры конфигурации токенов
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// секция в настройкаъ
    /// </summary>
    public const string OptionsKey = "JwtConfiguration";
    
    /// <summary>
    /// Кто запустил
    /// </summary>
    public string Issuer { get; set; }
    /// <summary>
    /// Доступные адреса
    /// </summary>
    public string Audience { get; set; }
    /// <summary>
    /// КЛюч
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// Время жизни токена авторизации
    /// </summary>
    public int JwtTokenExpirationTimeInMinutes { get; set; } = 2880;
    /// <summary>
    /// Время жизни токена обновения
    /// </summary>
    public int RefreshTokenExpirationTimeInHours { get; set; } = 240;
    
    /// <summary>
    /// Конструктор класса
    /// </summary>
    public JwtOptions()
    {
        Issuer = string.Empty;
        Audience = string.Empty;
        Key = string.Empty;
    }
}
