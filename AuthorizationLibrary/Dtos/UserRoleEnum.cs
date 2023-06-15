using System.Text.Json.Serialization;

namespace AuthorizationLibrary.Dtos;

/// <summary>
/// Возможные типы пользователя
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRoleEnum
{
    /// <summary>
    /// Обчный пользователь
    /// </summary>
    Regular,
    /// <summary>
    /// Администратор
    /// </summary>
    Admin
}
