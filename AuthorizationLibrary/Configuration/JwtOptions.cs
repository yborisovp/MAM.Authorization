namespace AuthorizationLibrary.Configuration;

public class JwtOptions
{
    /// <summary>
    /// секция в настройкаъ
    /// </summary>
    public const string OptionsKey = "JwtConfiguration";
    
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Key { get; set; }
    public int JwtTokenExpirationTimeInMinutes { get; set; } = 2880;
    public int RefreshTokenExpirationTimeInHours { get; set; } = 240;
    
    public JwtOptions()
    {
        Issuer = string.Empty;
        Audience = string.Empty;
        Key = string.Empty;
    }
}
