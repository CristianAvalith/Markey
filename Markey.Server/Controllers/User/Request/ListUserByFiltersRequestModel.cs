using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
namespace Markey.Server.Controllers.User.Request;

public class ListUserByFiltersRequestModel
{

    /// <example>Cristian Agente</example>
    [SwaggerSchema("FullName")]
    public string FullName { get; set; }

    /// <example>1</example>
    [SwaggerSchema("Page Number")]
    [JsonRequired]
    public int PageNumber { get; set; } = 1;

    /// <example>10</example>
    [SwaggerSchema("Page Size")]
    [JsonRequired]
    public int PageSize { get; set; } = 10;
}
