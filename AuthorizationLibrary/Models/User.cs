using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorizationLibrary.Models;

/// <summary>
/// Модель пользователя 
/// </summary>
public class User
{   /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [MaxLength(65)]
    public string Name { get; set; }
    
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [MaxLength(75)]
    public string SecondName { get; set; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime RegistrationDate { get; set; }
    
    /// <summary>
    /// Дата последнего изменения данных о пользователе
    /// </summary>
    public DateTime? LastModifiedDate { get; set; }
    
    /// <summary>
    /// Дата последнего входа
    /// </summary>
    public DateTime LastEnteredAt { get; set; }
    
    /// <summary>
    /// Дополнительная информация о пользователе
    /// </summary>
    [MaxLength(500)]
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Токен обновления
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
    /// <summary>
    /// Время жизни токена обновления
    /// </summary>
    public DateTimeOffset RefreshTokenExpirationDate { get; set; } = DateTimeOffset.MinValue;

    /// <summary>
    /// Виды пользовательской авторизации
    /// </summary>
    public virtual ICollection<UserCredential> Credentials { get; set; } = new List<UserCredential>();
}
