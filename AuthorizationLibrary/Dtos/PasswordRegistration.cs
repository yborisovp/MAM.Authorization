using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// Регистрация с паролем
/// </summary>
[JsonDerivedType(typeof(PasswordRegistration), typeDiscriminator: nameof(PasswordRegistration))]
public class PasswordRegistration : RegisterUserDto
{
    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [SwaggerSchema(Title = "Пароль")]
    public string Password { get; init; } = string.Empty;
}
