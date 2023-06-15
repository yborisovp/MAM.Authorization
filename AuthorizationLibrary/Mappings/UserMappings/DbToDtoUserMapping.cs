using AuthorizationLibrary.Dtos;
using AuthorizationLibrary.Models;

namespace AuthorizationLibrary.Mappings.UserMappings;

/// <summary>
/// Моедль БД в внешуюю модель
/// </summary>
public static class DbToDtoUserMapping
{
    /// <summary>
    /// Перевести в DTO
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            FullName = user.Name + " " + user.SecondName,
            RegistrationDate = user.RegistrationDate,
            LastModifiedDate = user.LastModifiedDate,
            LastEnteredAt = user.LastEnteredAt,
            AdditionalInfo = user.AdditionalInfo,
            Email = user.Credentials.FirstOrDefault()
                ?.Email,
            Role = user.Role.ToDto(),
            Id = user.Id
        };
    }
    /// <summary>
    /// Перевести в DTO
    /// </summary>
    /// <param name="user"></param>
    /// <param name="sessionToken"></param>
    /// <returns></returns>
    public static UserDto ToDto(this User user, string sessionToken)
    {
        return new UserDto
        {
            FullName = user.Name + " " + user.SecondName,
            RegistrationDate = user.RegistrationDate,
            LastModifiedDate = user.LastModifiedDate,
            LastEnteredAt = user.LastEnteredAt,
            AdditionalInfo = user.AdditionalInfo,
            AccessToken = sessionToken,
            Role = user.Role.ToDto(),
            Id = user.Id
        };
    }

    private static UserRoleEnum ToDto(this UserRole role)
    {
        return role switch
        {

            UserRole.Regular => UserRoleEnum.Regular,
            UserRole.Admin => UserRoleEnum.Admin,
            _ => throw new KeyNotFoundException("Cannot found this role type")
        };
    }
}
