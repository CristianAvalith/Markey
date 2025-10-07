namespace Markey.Domain.Models;

public class TokenResponse
{
    public Guid UserId { get; set; }
    public string AccessToken { get; set; }
}
