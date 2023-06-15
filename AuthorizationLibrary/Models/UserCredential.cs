using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthorizationLibrary.Models.AuthorizationProviders;

namespace AuthorizationLibrary.Models;

/// <summary>
/// Пользовательские данные для регистрации
/// </summary>
public class UserCredential
{
    /// <summary>
    /// Уникальный идентификатор пользовательских регистрационных данных
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    /// <summary>
    /// Электронная почта
    /// </summary>
    [EmailAddress]
    public required string? Email { get; set; }

    /// <summary>
    /// Провайдеры авторизации
    /// </summary>
    public virtual AuthorizationProvider AuthorizationProviders { get; set; } = null!;
    
    /// <summary>
    /// Id пльзователя
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// Обхект пользователя
    /// </summary>
    public virtual User User { get; set; } = null!;
}
