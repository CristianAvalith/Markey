using Swashbuckle.AspNetCore.Annotations;
namespace Markey.Server.Controllers.Auth.Request;

[SwaggerSchema("", Required = ["UserName", "Pasword"])]
public class RegisterRequestModel
{
    /// <example>Cris98n</example>
    [SwaggerSchema("User Name")]
    public string UserName { get; set; }

    /// <example>cristian.fer.mel@gmail.com</example>
    [SwaggerSchema("User's email")]
    public string Email { get; set; }

    /// <example>Cristian Fernandez</example>
    [SwaggerSchema("User's Name")]
    public string FullName { get; set; }

    /// <example>28134207-94C2-4B40-808A-0A98AA0BC3E4</example>
    [SwaggerSchema("User's occupation Id")]
    public Guid OccupationId { get; set; }

    /// <example>1134778956</example>
    [SwaggerSchema("Phone Number")]
    public string PhoneNumber { get; set; }

    /// <example>Password_123</example>
    [SwaggerSchema("User's password")]
    public string Password { get; set; }

    public IFormFile Photo { get; set; }
}