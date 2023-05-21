using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// DTO ползователя
/// </summary>
[SwaggerSchema(Title = "DTO пользователя")]
public class UserDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    [MaxLength(65)]
    [SwaggerSchema(Title = "Полное имя пользователя")]
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Дата регистрации
    /// </summary>
    [DataType(DataType.DateTime)]
    [SwaggerSchema(Title = "Дата регистрации")]
    public DateTime RegistrationDate { get; init; }
    
    /// <summary>
    /// Дата последнего изменения данных о пользователе
    /// </summary>
    [DataType(DataType.DateTime)]
    [SwaggerSchema(Title = "Дата последнего изменения данных о пользователе")]
    public DateTime? LastModifiedDate { get; init; }

    /// <summary>
    /// Дата последнего входа
    /// </summary>
    [SwaggerSchema(Title = "Дата последнего входа")]
    public DateTime LastEnteredAt { get; init; }

    /// <summary>
    /// Дополнительная информация о пользователе
    /// </summary>
    [MaxLength(500)]
    [SwaggerSchema(Title = "Дополнительная информация о пользователе")]
    public string? AdditionalInfo { get; init; }

    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

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
    
    [Required]
    [MaxLength(70)]
    [SwaggerSchema(Title = "Имя пользователя")]
    public string Name { get; init; }= string.Empty;
    
    [Required]
    [MaxLength(70)]
    [SwaggerSchema(Title = "Фамилия пользователя")]
    public string SecondName { get; init; }= string.Empty;
}

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
    /// Токен внешней авторизации
    /// </summary>
    [Required]
    [SwaggerSchema(Title = "Токен внешней авторизации")]
    public string OAuthToken { get; set; } = string.Empty;
    /// <summary>
    /// Уникальный идентификатор клиента во внешней системе
    /// </summary>
    [Required]
    [SwaggerSchema(Title = "Уникальный идентификатор клиента во внешней системе")]
    public string OAuthClientId { get; set; } = string.Empty;
}

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

[SwaggerSchema(Title = "DTO для обновления")]
public class UpdateUserDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [MaxLength(65)]
    [SwaggerSchema(Title = "Имя пользователя")]
    public string Name { get; init; } = string.Empty;
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [MaxLength(75)]
    [SwaggerSchema(Title = "Фамилия пользователя")]
    public string SecondName { get; init; } = string.Empty;

    /// <summary>
    /// Дополнительная информация о пользователе
    /// </summary>
    [MaxLength(500)]
    [SwaggerSchema(Title = "Дополнительная информация о пользователе")]
    public string? AdditionalInfo { get; init; }
}

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