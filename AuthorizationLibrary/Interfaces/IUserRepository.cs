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
    /// Найти пользователя по Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<User> GetOnlyUserByIdAsync(long id, CancellationToken ct);
    
    /// <summary>
    /// Найти пользователей по Id
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<ICollection<User>> GetUsersById(IEnumerable<long> ids, CancellationToken ct = default);
    
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
    /// <summary>
    /// Отозвать существующий токен из системы авторизации
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task RevokeToken(long userId, CancellationToken ct);
}
