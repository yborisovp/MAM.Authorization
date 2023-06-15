using AuthorizationLibrary.Models;
using AuthorizationLibrary.Models.AuthorizationProviders;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationLibrary.Context;

/// <summary>
/// Контекст для взаимодействия с базой данных пользователя
/// </summary>
public class AuthorizationDatabaseContext : DbContext
{
    /// <summary>
    /// Описание схемы 
    /// </summary>
    public const string AuthorizationSchema = "Authorization";
    /// <summary>
    /// Имя таблицы, в которую будут записываться миграции
    /// </summary>
    public const string DefaultMigrationHistoryTableName = "__MigrationsHistory";
    
    /// <summary>
    /// Конструктор контекста
    /// </summary>
    /// <param name="options"></param>
    public AuthorizationDatabaseContext(DbContextOptions<AuthorizationDatabaseContext> options)
        : base(options)
    {
    }
    
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Авторизационные данные
    /// </summary>
    public DbSet<UserCredential> Credentials { get; set; } = null!;

    /// <summary>
    /// Пререопределение создания модели
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCredential>()
                    .HasIndex(u => u.Email)
                    .IsUnique();
        
        modelBuilder.HasDefaultSchema(AuthorizationSchema);
        
        modelBuilder.Entity<ExternalAuthorizationProvider>()
                    .HasBaseType<AuthorizationProvider>();
        modelBuilder.Entity<PasswordAuthorizationProvider>()
                    .HasBaseType<AuthorizationProvider>();
        
        modelBuilder.Entity<UserCredential>()
                    .ToTable("Credentials");
        modelBuilder.Entity<User>()
                    .ToTable("Users");
            
        base.OnModelCreating(modelBuilder);
    }
}
