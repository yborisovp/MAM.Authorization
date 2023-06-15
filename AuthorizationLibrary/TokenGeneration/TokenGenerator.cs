using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthorizationLibrary.Configuration;
using AuthorizationLibrary.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationLibrary.TokenGeneration;

/// <summary>
/// Класс для создания токенов
/// </summary>
public class TokenGenerator: ITokenGenerator
{
    
    private readonly JwtOptions _jwtOptions;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="jwtOptions"></param>
    public TokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    private const string DefaultSecurityKey = "this is my custom Secret key for authentication";
    
    /// <summary>
    /// Создать новый токен доступа
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key.IsNullOrEmpty() ? DefaultSecurityKey : _jwtOptions.Key));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        {
            new ("userId", user.Id.ToString()),
            new ("name", user.Name + " " + user.SecondName),
        };

        var jwtTokenExpirationTime = DateTimeOffset.Now.AddMinutes(_jwtOptions.JwtTokenExpirationTimeInMinutes);

        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            jwtTokenExpirationTime.DateTime,
            signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }

    /// <summary>
    /// Отозвать токен
    /// </summary>
    public void RevokeToken()
    {
        var jwtTokenExpirationTime = DateTimeOffset.Now.AddMinutes(-10);

        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            null,
            null,
            jwtTokenExpirationTime.DateTime);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    /// <summary>
    /// Создать токен обновления
    /// </summary>
    /// <returns></returns>
    public RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            TokenExpirationDate = DateTimeOffset.UtcNow.AddHours(_jwtOptions.RefreshTokenExpirationTimeInHours)
        };
    }
}
