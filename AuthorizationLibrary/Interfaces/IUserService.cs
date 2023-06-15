using System.Security.Claims;
using AuthorizationLibrary.Dtos;
using AuthorizationLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationLibrary.Interfaces;

/// <summary>
/// Интерфейс сервиса для взаимодействия с бд
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <param name="registerUserDto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<UserDto> RegisterUserAsync(RegisterUserDto registerUserDto, CancellationToken ct);
    /// <summary>
    /// Обновить пользовательские данные
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userDto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<UserDto> UpdateUserAsync(long id, UpdateUserDto userDto, CancellationToken ct);
    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<bool> DeleteUserAsync(long id, CancellationToken ct);
    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="loginUserDto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<UserDto> LoginUserAsync(LoginUserDto loginUserDto, CancellationToken ct);
    /// <summary>
    /// Убрать авторизацию пользователя
    /// </summary>
    /// <param name="userClaims"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task LogoutUser(IEnumerable<Claim> userClaims, CancellationToken ct);

    Task<UserDto> GetUserById(long userId, CancellationToken ct);
    
}
