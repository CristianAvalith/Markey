using Swashbuckle.AspNetCore.Annotations;
namespace Markey.Server.Controllers.Auth.Request;

[SwaggerSchema("", Required = ["UserName", "Pasword"])]
public class LoginRequestModel
{
    /// <example>admin</example>
    [SwaggerSchema("User's username")]
    public string UserName { get; set; }

    /// <example>Password_123</example>
    [SwaggerSchema("User's password")]
    public string Password { get; set; }
}
