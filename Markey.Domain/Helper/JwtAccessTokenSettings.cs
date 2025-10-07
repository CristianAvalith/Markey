using Markey.Domain.Interfaces;

namespace Markey.Domain.Helper;
public class JwtAccessTokenSettings : IJwtSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
