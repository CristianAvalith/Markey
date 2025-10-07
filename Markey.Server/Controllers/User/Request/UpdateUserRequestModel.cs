using Swashbuckle.AspNetCore.Annotations;
namespace Markey.Server.Controllers.User.Request;
public class UpdateUserRequestModel
{

    /// <example>Cristian Fernandez</example>
    [SwaggerSchema("User's Name")]
    public string FullName { get; set; }

    /// <example>24c41869-6627-460e-9609-3c5095c6e6ef</example>
    [SwaggerSchema("User's occupation")]
    public Guid OccupationId { get; set; }

    /// <example>1134778956</example>
    [SwaggerSchema("Phone Number")]
    public string PhoneNumber { get; set; }
}
