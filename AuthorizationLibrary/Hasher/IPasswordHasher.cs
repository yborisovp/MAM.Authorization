namespace WebApi.Services.Hasher;

/// <summary>
/// Интерфейс для взаимодействия с сервисом хеширования паролей
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Захешировать пароль
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string Hash(string password);
    /// <summary>
    /// Подтвердить пароль
    /// </summary>
    /// <param name="password"></param>
    /// <param name="inputPassword"></param>
    /// <returns></returns>
    public bool Verify(string password, string inputPassword);
}
