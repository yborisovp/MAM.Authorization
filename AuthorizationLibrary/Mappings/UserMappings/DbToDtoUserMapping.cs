using AuthorizationLibrary.Dtos;
using AuthorizationLibrary.Models;

namespace AuthorizationLibrary.Mappings.UserMappings;

public static class DbToDtoUserMapping
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            FullName = user.Name + " " + user.SecondName,
            RegistrationDate = user.RegistrationDate,
            LastModifiedDate = user.LastModifiedDate,
            LastEnteredAt = user.LastEnteredAt,
            AdditionalInfo = user.AdditionalInfo
        };
    }
    public static UserDto ToDto(this User user, string sessionToken)
    {
        return new UserDto
        {
            FullName = user.Name + " " + user.SecondName,
            RegistrationDate = user.RegistrationDate,
            LastModifiedDate = user.LastModifiedDate,
            LastEnteredAt = user.LastEnteredAt,
            AdditionalInfo = user.AdditionalInfo,
            AccessToken = sessionToken
        };
    }
    
}
