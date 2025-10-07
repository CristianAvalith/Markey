using AutoMapper;
using Markey.Application.Catalog.GetOccupations;
using Markey.CrossCutting.Helpers;
using Markey.CrossCutting.Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Markey.Server.Controllers.Catalog;

[ApiController]
[Route("catalog")] 
public class CatalogController : ControllerBase
{
    private readonly Mediator _mediator;
    private readonly IMapper _mapper;
    private const string InternalServerError = "An unexpected error occurred.";

    public CatalogController(Mediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    #region [GET] Get Occupations
    [HttpGet("occupations")]
    [SwaggerOperation(Summary = "Método para obtener la lista de ocupaciones disponibles.")]
    public async Task<IActionResult> GetOccupations()
    {
        try
        {
            var result = await _mediator.Send(new GetOccupationsRequest());

            return result.Code switch
            {
                (int)Code.SUCCESS => Ok(result),
                (int)Code.SERVICE_UNAVAILABLE => StatusCode(StatusCodes.Status503ServiceUnavailable, result),
                _ => StatusCode(StatusCodes.Status503ServiceUnavailable, result)
            };
        }
        catch (Exception)
        {
            return StatusCode(500, new { Error = InternalServerError });
        }
    }
    #endregion
}
