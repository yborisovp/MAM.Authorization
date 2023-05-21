using System.Text;
using AuthorizationLibrary.Context;
using AuthorizationLibrary.Interfaces;
using AuthorizationLibrary.Repository;
using AuthorizationLibrary.Services;
using AuthorizationLibrary.TokenGeneration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services.Hasher;

namespace AuthorizationLibrary.Configuration;

/// <summary>
/// Класс для применения конфигурации
/// </summary>
public static class MamAuthorizationConfiguration
{
    /// <summary>
    /// Добавление конфигурации в сборщик
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="configurationManager"></param>
    public static void AddMamAuthorization(this IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        var jwtOptions = configurationManager.GetSection(JwtOptions.OptionsKey);
        serviceCollection.Configure<JwtOptions>(jwtOptions);
        
        var authorizationDatabaseOptions = configurationManager.GetSection(AuthorizationDatabaseOptions.DefaultName).Get<AuthorizationDatabaseOptions>();
        if (authorizationDatabaseOptions is null)
        {
            throw new ArgumentNullException(nameof(authorizationDatabaseOptions));
        }
        serviceCollection.AddScoped<IAuthorizationDatabaseContextFactory>(_ => new AuthorizationDatabaseContextFactory(authorizationDatabaseOptions.ConnectionString));
        serviceCollection.AddDbContext<AuthorizationDatabaseContext>(options => options.UseNpgsql(authorizationDatabaseOptions.ConnectionString,
            x => x.MigrationsHistoryTable(
                AuthorizationDatabaseContext.DefaultMigrationHistoryTableName,
                AuthorizationDatabaseContext.AuthorizationSchema
            )).UseSnakeCaseNamingConvention());
        
        serviceCollection.AddScoped<ITokenGenerator, TokenGenerator>();
        serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();
        
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IAuthorizationService, AuthorizationService>();

        var jwtOptionsSettings = jwtOptions.Get<JwtOptions>();
        if (jwtOptionsSettings is null)
        {
            throw new ArgumentNullException(nameof(jwtOptionsSettings));
        }
        
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptionsSettings?.Issuer,
                ValidAudience = jwtOptionsSettings?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsSettings?.Key))
            };
        });
        
    }
    
}
