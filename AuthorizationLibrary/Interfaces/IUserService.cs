using AuthorizationLibrary.Dtos;
using AuthorizationLibrary.Models;

namespace AuthorizationLibrary.Interfaces;

/// <summary>
/// Интерфейс сервиса для взаимодействия с бд
/// </summary>
public interface IUserService
{
    public Task<UserDto> RegisterUserAsync(RegisterUserDto registerUserDto, CancellationToken ct);
    public Task<UserDto> UpdateUserAsync(long id, UpdateUserDto userDto, CancellationToken ct);
    public Task<bool> DeleteUserAsync(long id, CancellationToken ct);
    public Task<UserDto> LoginUserAsync(LoginUserDto loginUserDto, CancellationToken ct);
}
