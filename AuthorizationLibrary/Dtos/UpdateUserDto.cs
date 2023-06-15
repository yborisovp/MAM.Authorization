using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// Новые  данные пользователя
/// </summary>
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
