using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorizationLibrary.Models.AuthorizationProviders;

/// <summary>
/// Базовый класс авторизации
/// </summary>
public abstract class AuthorizationProvider
{
    /// <summary>
    /// Уникальный идентификатор провайдера авторизации
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор пользовательских регистрационных данных
    /// </summary>
    public long UserCredentialId { get; set; }
}
