using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// DTO для регистрации
/// </summary>
[JsonDerivedType(typeof(ThirdPartyRegistration), typeDiscriminator: nameof(ThirdPartyRegistration))]
[JsonDerivedType(typeof(PasswordRegistration), typeDiscriminator: nameof(PasswordRegistration))]
[SwaggerSchema(Title = "DTO для регистрации")]
public abstract class RegisterUserDto
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    [Required]
    [MaxLength(70)]
    [SwaggerSchema(Title = "Электронная почта")]
    public string Email { get; init; } = string.Empty;
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    [MaxLength(70)]
    [SwaggerSchema(Title = "Имя пользователя")]
    public string Name { get; init; }= string.Empty;
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [Required]
    [MaxLength(70)]
    [SwaggerSchema(Title = "Фамилия пользователя")]
    public string SecondName { get; init; }= string.Empty;
}
