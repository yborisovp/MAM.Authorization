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
    
}

/// <summary>
/// Типы поддерживаемой авторизации
/// </summary>
public enum ExternalAuthorizationType
{
    /// <summary>
    /// Гугл
    /// </summary>
    Google,
    /// <summary>
    /// Яндекс
    /// </summary>
    Yandex,
    /// <summary>
    /// ВКонтакте
    /// </summary>
    Vk
}
