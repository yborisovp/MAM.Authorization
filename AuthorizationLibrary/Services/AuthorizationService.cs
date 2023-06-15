using AuthorizationLibrary.Interfaces;
using AuthorizationLibrary.Models;
using AuthorizationLibrary.TokenGeneration;

namespace AuthorizationLibrary.Services;

/// <summary>
/// Сервис авторизации
/// </summary>
public class AuthorizationService: IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    
    /// <summary>
    /// Конструктор сервиса авторизации
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="tokenGenerator"></param>
    public AuthorizationService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    /// <summary>
    /// Авторизовать пользователя в текущем сервисе
    /// </summary>
    /// <param name="email"></param>
    /// <param name="refreshToken"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<string> GetAccessTokenAsync(string email, string refreshToken,  CancellationToken ct)
    {
        var user = await _userRepository.GetUserByEmail(email, ct);
        if (user is null)
        {
            throw new KeyNotFoundException($"User with this email: '{email}' cannot be found");
        }

        if (!user.RefreshToken.Equals( refreshToken, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidDataException("Refresh tokens does not match");
        }
        
        var jwt = _tokenGenerator.GenerateJwt(user);
        return jwt;
    }
    
    /// <summary>
    /// Обновить токен пользователя для сервиса
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="email"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<string> RefreshTokenAsync(string refreshToken, string email, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByEmail(email, ct);
        if (user is null)
        {
            throw new KeyNotFoundException($"User with email: '{email}' cannot be found");
        }
        CheckRefreshToken(refreshToken, user);

        return _tokenGenerator.GenerateJwt(user);
    }
    
    private static void CheckRefreshToken(string refreshToken, User user)
    {
        if (!user.RefreshToken.Equals(refreshToken))
        {
            throw new KeyNotFoundException("Invalid refresh token has been provided.");
        }

        if (user.RefreshTokenExpirationDate < DateTimeOffset.Now)
        {
            throw new InvalidDataException("Refresh token is expired.");
        }
    }
}
