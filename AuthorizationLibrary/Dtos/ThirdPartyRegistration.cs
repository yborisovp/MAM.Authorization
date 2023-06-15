using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// Регистрация с помощью внешней авторизации
/// </summary>
[JsonDerivedType(typeof(ThirdPartyRegistration), typeDiscriminator: nameof(ThirdPartyRegistration))]
public class ThirdPartyRegistration : RegisterUserDto
{
    /// <summary>
    /// Тип внешней авторизации
    /// </summary>
    [Required]
    public ThirdPartyAuthorizationType AuthorizationType { get; set; }
    /// <summary>
    /// Код внешней авторизации
    /// </summary>
    [Required]
    [SwaggerSchema(Title = "Код внешней авторизации")]
    public required string OAuthCode { get; set; }
}
