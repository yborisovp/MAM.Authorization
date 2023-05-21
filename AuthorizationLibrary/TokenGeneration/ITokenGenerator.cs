using AuthorizationLibrary.Models;

namespace AuthorizationLibrary.TokenGeneration;

public interface ITokenGenerator
{
    string GenerateJwt(User user);
    RefreshToken GenerateRefreshToken();
}
