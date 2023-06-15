using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// DTO пользователя
/// </summary>
[SwaggerSchema(Title = "DTO пользователя")]
public class UserDto
{
    public required long Id { get; set; }
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

    /// <summary>
    /// Роль пользователя
    /// </summary>
    [Required]
    public required UserRoleEnum Role { get; set; }
    /// <summary>
    /// Токен доступа
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;
    /// <summary>
    /// Токен обнавления
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    public string? Email { get; set; } = null!;
}
