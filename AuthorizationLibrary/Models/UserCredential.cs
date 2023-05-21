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
    [MaxLength(70)]
    public required string Email { get; set; }

    /// <summary>
    /// Провайдеры авторизации
    /// </summary>
    public virtual AuthorizationProvider AuthorizationProviders { get; set; } = null!;
    
    public long UserId { get; set; }
    public virtual User User { get; set; }
}
