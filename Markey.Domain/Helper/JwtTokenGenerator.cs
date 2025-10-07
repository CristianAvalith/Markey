using Markey.Domain.Interfaces;
using Markey.Domain.Models;
using Markey.Persistance.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Markey.Domain.Helper;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtAccessTokenSettings _jwtAccessTokenSettings;
    private readonly IConfiguration _config;

    public JwtTokenGenerator(IOptions<JwtAccessTokenSettings> jwtAccessTokenSettings, IConfiguration config)
    {
        _jwtAccessTokenSettings = jwtAccessTokenSettings.Value;
        _config = config;
    }

    public async Task<TokenResponse> GenerateSessionTokenAsync(User user)
    {
        var accessTokenKey = Encoding.ASCII.GetBytes(_config["SecretKey"]);

        var claims = new[]
        {
             new Claim("UserId", user.Id.ToString()),
             new Claim("Email", user.Email.ToString()),
        };

        string accessToken = await GenerateToken(claims, DateTime.UtcNow.AddDays(5), accessTokenKey, _jwtAccessTokenSettings);

        TokenResponse tokens = new()
        {
            AccessToken = accessToken,
            UserId = user.Id
        };

        return tokens;
    }

    private static Task<string> GenerateToken(IEnumerable<Claim> claims, DateTime dateTime, byte[] key, IJwtSettings settings)
    {

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = dateTime,
            Issuer = "Markey",
            Audience = "Markey",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}
