using AuthorizationLibrary.Models;

namespace AuthorizationLibrary.Interfaces;

/// <summary>
/// Интерфейс репозитория пользователей
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Найти пользователя по email
    /// </summary>
    /// <param name="email">email пользователя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    Task<User> GetUserByEmail(string email, CancellationToken ct);
    
    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <param name="user">Модель пользователя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public Task<User> RegisterUserAsync(User user, CancellationToken ct);
    
    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="user">Модель пользователя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public Task<User> UpdateUserAsync(User user, CancellationToken ct);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">ID пользователя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public Task<bool> DeleteUserAsync(long id, CancellationToken ct);
}
