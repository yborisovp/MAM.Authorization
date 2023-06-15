using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// Вход с помощью внешней авторизации
/// </summary>
[JsonDerivedType(typeof(ThirdPartyLogin), typeDiscriminator: nameof(ThirdPartyLogin))]
public class ThirdPartyLogin : LoginUserDto
{
    /// <summary>
    /// Токен внешней авторизации
    /// </summary>
    [SwaggerSchema(Title = "Токен внешней авторизации")]
    public required string OAuthToken { get; set; }
    /// <summary>
    /// Уникальный идентификатор клиента во внешней системе
    /// </summary>
    [SwaggerSchema(Title = "Уникальный идентификатор клиента во внешней системе")]
    public required string OAuthClientId { get; set; }
}
