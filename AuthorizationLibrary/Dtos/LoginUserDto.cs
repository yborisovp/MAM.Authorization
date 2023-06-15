using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// DTO для входа
/// </summary>
[JsonDerivedType(typeof(ThirdPartyLogin), typeDiscriminator: nameof(ThirdPartyLogin))]
[JsonDerivedType(typeof(PasswordLoginDto), typeDiscriminator: nameof(PasswordLoginDto))]
[SwaggerSchema(Title = "DTO для входа")]
public abstract class LoginUserDto
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    [Required]
    [MaxLength(70)]
    [SwaggerSchema(Title = "Электронная почта")]
    public string Email { get; init; } = string.Empty;
}
