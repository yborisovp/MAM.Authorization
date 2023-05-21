namespace AuthorizationLibrary.Configuration;

/// <summary>
/// Настройки базы данных для авторизации
/// </summary>
public class AuthorizationDatabaseOptions
{
    /// <summary>
    /// Имя секции по умолчанию
    /// </summary>
    public const string DefaultName = "AuthorizationDatabaseOptions";
    /// <summary>
    /// Подключение к базе данных
    /// </summary>
    public required string ConnectionString { get; set; }
}
