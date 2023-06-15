using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// Вход с паролем
/// </summary>
[JsonDerivedType(typeof(PasswordLoginDto), typeDiscriminator: nameof(PasswordLoginDto))]
public class PasswordLoginDto : LoginUserDto
{
    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [SwaggerSchema(Title = "Пароль")]
    public string Password { get; init; } = string.Empty;
}
