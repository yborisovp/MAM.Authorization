using System.Text.Json;
using System.Text.Json.Serialization;
using AuthorizationLibrary.Context;
using AuthorizationLibrary.Interfaces;
using AuthorizationLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationLibrary.Repository;

internal class UserRepository : BaseRepository, IUserRepository
{

    public UserRepository(IAuthorizationDatabaseContextFactory contextFactory) : base(contextFactory)
    {
    }
    
    public async Task<User> GetUserByEmail(string email, CancellationToken ct)
    {
        await using var context = ContextFactory.CreateDbContext();
        var user = await GetFullUserQuery(context.Users).FirstOrDefaultAsync(u => u.Credentials.Any(c => c.Email == email), ct);

        if (user is null)
        {
            throw new KeyNotFoundException($"User with this email: '{email}' cannot be found");
        }
        
        return user;
    }

    public async Task<User> GetOnlyUserByIdAsync(long id, CancellationToken ct)
    {
        await using var context = ContextFactory.CreateDbContext();
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        if (user is null)
        {
            throw new KeyNotFoundException($"User with this id: '{id}' cannot be found");
        }
        
        return user;
    }
    
    public async Task<ICollection<User>> GetUsersById(IEnumerable<long> ids, CancellationToken ct = default)
    {
        await using var context = ContextFactory.CreateDbContext();
        var users = await GetFullUserQuery(context.Users).Where(u => ids.Contains(u.Id)).ToListAsync(ct);
        if (!users.Any())
        {
            throw new KeyNotFoundException($"Cannot found users with provided IDs: '{JsonSerializer.Serialize(ids)}'");
        }
        return users;
    }

    public async Task<User> RegisterUserAsync(User user, CancellationToken ct)
    {
        await using var context = ContextFactory.CreateDbContext();
        var newUser =await context.Users.AddAsync(user, ct);
        await context.SaveChangesAsync(ct);
        return newUser.Entity;
    }
    public async Task<User> UpdateUserAsync(User user, CancellationToken ct)
    {
        await using var context = ContextFactory.CreateDbContext();
        var entryEntity = context.Entry(user);
        entryEntity.State = EntityState.Modified;
        entryEntity.Property(u => u.Id).IsModified = false;
        entryEntity.Property(u => u.RegistrationDate).IsModified = false;
        foreach (var userCredential in user.Credentials)
        {
            context.Entry(userCredential).State = EntityState.Unchanged;
        }
        await context.SaveChangesAsync(ct);
        return user;
    }
    public async Task<bool> DeleteUserAsync(long id, CancellationToken ct)
    {
        await using var context = ContextFactory.CreateDbContext();
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

        if (user is null)
        {
            throw new KeyNotFoundException($"User with this id: '{id}' cannot be found");
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync(ct);
        return true;
    }
    public async Task RevokeToken(long userId, CancellationToken ct)
    {
        await using var context = ContextFactory.CreateDbContext();
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (user is null)
        {
            throw new KeyNotFoundException($"User with this id: '{userId}' cannot be found");
        }
        
        user.RefreshToken = string.Empty;
        user.RefreshTokenExpirationDate = DateTimeOffset.Now;

        context.Users.Update(user);
        await context.SaveChangesAsync(ct);
    }

    private static IQueryable<User> GetFullUserQuery(DbSet<User> user)
    {
        return user.Include(u => u.Credentials)
                   .ThenInclude(c => c.AuthorizationProviders);
    }
}
