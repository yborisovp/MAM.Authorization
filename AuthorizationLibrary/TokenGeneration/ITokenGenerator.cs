using AuthorizationLibrary.Models;

namespace AuthorizationLibrary.TokenGeneration;

/// <summary>
/// Интерфейс для создания JWT токенов
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Создать новый токен доступа
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    string GenerateJwt(User user);
    
    /// <summary>
    /// Создать новый токен обновления
    /// </summary>
    /// <returns></returns>
    RefreshToken GenerateRefreshToken();

    /// <summary>
    /// Отозвать текущий токен
    /// </summary>
    void RevokeToken();
}
