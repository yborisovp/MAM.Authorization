using AuthorizationLibrary.Dtos;
using AuthorizationLibrary.Models;
using AuthorizationLibrary.Models.AuthorizationProviders;

namespace AuthorizationLibrary.Mappings.UserMappings;

/// <summary>
/// Превести внешнюю модель пользователя в модельь для базы данных
/// </summary>
public static class DtoToDbUserMappings
{
    /// <summary>
    /// Превести внешнюю модель пользователя к модели регистрации
    /// </summary>
    /// <param name="registerUserDto"></param>
    /// <param name="passwordHash"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    public static User ToModel(this RegisterUserDto registerUserDto, string? passwordHash = null)
    {
        return registerUserDto switch
        {
            PasswordRegistration passwordRegistration => passwordHash is null ? throw new ArgumentNullException(passwordHash, "Password requried in authorization with password") : new User
            {
                Name = passwordRegistration.Name,
                SecondName = passwordRegistration.SecondName,
                RegistrationDate = DateTime.UtcNow,
                LastEnteredAt = DateTime.UtcNow,
                Credentials = new List<UserCredential>()
                {
                    new()
                    {
                        Email = registerUserDto.Email,
                        AuthorizationProviders = new PasswordAuthorizationProvider
                        {
                            PasswordHash = passwordHash,
                            LastModifiedTime = DateTime.UtcNow
                        }
                    }
                },
                Role = UserRole.Regular
            },
            ThirdPartyRegistration thirdPartyRegistration => new User
            {
                Name = thirdPartyRegistration.Name,
                SecondName = thirdPartyRegistration.SecondName,
                RegistrationDate = DateTime.UtcNow,
                Credentials = new List<UserCredential>()
                {
                    new()
                    {
                        Email = registerUserDto.Email,
                        AuthorizationProviders = new ExternalAuthorizationProvider
                        {
                            AuthorizationType = thirdPartyRegistration.AuthorizationType.ToAuthorizationTypeModel(),
                            OAuthToken = thirdPartyRegistration.OAuthCode,
                        }
                    }
                },
                Role = UserRole.Regular
            },
            _ => throw new KeyNotFoundException("Not supported registration type")

        };
    }

    private static ExternalAuthorizationType ToAuthorizationTypeModel(this ThirdPartyAuthorizationType type)
    {
        return type switch
        {
            ThirdPartyAuthorizationType.Google => ExternalAuthorizationType.Google,
            ThirdPartyAuthorizationType.Yandex => ExternalAuthorizationType.Yandex,
            ThirdPartyAuthorizationType.Vk => ExternalAuthorizationType.Vk,
            _ => throw new KeyNotFoundException("This external authorization type is not supported")
        };
    }
}
