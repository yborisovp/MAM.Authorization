using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// Поддерживаемые типы авторизации
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
[SwaggerSchema(Title = "Поддерживаемые типы авторизации")]
public enum ThirdPartyAuthorizationType
{
    /// <summary>
    /// Google
    /// </summary>
    Google,
    /// <summary>
    /// Яндекс
    /// </summary>
    Yandex,
    /// <summary>
    /// Вконтакте
    /// </summary>
    Vk
}
