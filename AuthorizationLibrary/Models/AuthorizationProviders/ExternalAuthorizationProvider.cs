namespace AuthorizationLibrary.Models.AuthorizationProviders;

/// <summary>
/// Авторизация с помощью внешних сервисов
/// </summary>
public class ExternalAuthorizationProvider : AuthorizationProvider
{
    /// <summary>
    /// Тип авторизации
    /// </summary>
    public ExternalAuthorizationType AuthorizationType { get; set; }
    /// <summary>
    /// Токен внешней авторизации
    /// </summary>
    public required string OAuthToken { get; set; }
    /// <summary>
    /// Уникальный идентификатор клиента во внешней системе
    /// </summary>
    public required string OAuthClientId { get; set; }
}

/// <summary>
/// Типы поддерживаемой авторизации
/// </summary>
public enum ExternalAuthorizationType
{
    Google,
    Yandex,
    Vk
}
