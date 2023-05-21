using AuthorizationLibrary.Dtos;
using AuthorizationLibrary.Interfaces;
using AuthorizationLibrary.Mappings.UserMappings;
using AuthorizationLibrary.Models;
using AuthorizationLibrary.Models.AuthorizationProviders;
using AuthorizationLibrary.TokenGeneration;
using WebApi.Services.Hasher;

namespace AuthorizationLibrary.Services;

/// <summary>
/// Сервис взаимодействия с пользователем
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    /// <summary>
    /// Конструктор сервиса для взаимодействия с данными пользователя
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="passwordHasher"></param>
    /// <param name="tokenGenerator"></param>
    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="loginUserDto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<UserDto> LoginUserAsync(LoginUserDto loginUserDto, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByEmail(loginUserDto.Email, ct);
        if (user is null)
        {
            throw new KeyNotFoundException($"Cannot found a user with email: '{loginUserDto.Email}'");
        }
        if (loginUserDto is PasswordLoginDto passwordLoginDto)
        {
            var userPasswordCredentials = user.Credentials.Select(c => c.AuthorizationProviders).ToList();
            var a = userPasswordCredentials.FirstOrDefault(c => c is PasswordAuthorizationProvider) as PasswordAuthorizationProvider;
            if (userPasswordCredentials is null)
            {
                throw new KeyNotFoundException($"For user '{loginUserDto.Email}' password credentials doesn't not found");
            }
            
            var isPasswordValid = _passwordHasher.Verify(a.PasswordHash, passwordLoginDto.Password);

            if (!isPasswordValid)
            {
                
            }
        }

        var jwt = _tokenGenerator.GenerateJwt(user);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        
        await AssignRefreshToken(refreshToken, user);

        var dto = user.ToDto();
        dto.AccessToken = jwt;
        dto.RefreshToken = refreshToken.Token;
        return dto;
    }
    
    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <param name="registerUserDto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<UserDto> RegisterUserAsync(RegisterUserDto registerUserDto, CancellationToken ct)
    {
        User user;
        if (registerUserDto is PasswordRegistration passwordLoginDto)
        {
            var passwordHash = _passwordHasher.Hash(passwordLoginDto.Password);
            user = registerUserDto.ToModel(passwordHash);
        }
        else
        {
            user = registerUserDto.ToModel();
        }

        await _userRepository.RegisterUserAsync(user, ct);
        var jwt = _tokenGenerator.GenerateJwt(user);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        
        await AssignRefreshToken(refreshToken, user);

        var dto = user.ToDto();
        dto.AccessToken = jwt;
        dto.RefreshToken = refreshToken.Token;
        
        return dto;
    }
    public async Task<UserDto> UpdateUserAsync(long id, UpdateUserDto userDto, CancellationToken ct) => throw new NotImplementedException();
    public async Task<bool> DeleteUserAsync(long id, CancellationToken ct) => throw new NotImplementedException();
    
    private static void CheckRefreshToken(string refreshToken, User user)
    {
        if (!user.RefreshToken.Equals(refreshToken))
        {
            throw new KeyNotFoundException("Invalid refresh token has been provided.");
        }

        if (user.RefreshTokenExpirationDate < DateTimeOffset.Now)
        {
            throw new KeyNotFoundException("Refresh token is expired.");
        }
    }
    private async Task AssignRefreshToken(RefreshToken refreshToken, User user)
    {
        user.RefreshToken = refreshToken.Token;
        user.RefreshTokenExpirationDate = refreshToken.TokenExpirationDate;

        await _userRepository.UpdateUserAsync(user, default);
    }
}
