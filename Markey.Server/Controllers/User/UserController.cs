using AutoMapper;
using Markey.Application.User.GetUser;
using Markey.Application.User.List.ListUsers;
using Markey.Application.User.Update;
using Markey.Application.User.UpdatePhoto;
using Markey.CrossCutting.Helpers;
using Markey.CrossCutting.Mediator;
using Markey.Server.Controllers.User.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace Markey.Server.Controllers.User;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly Mediator _mediator;
    private readonly IMapper _mapper;
    private const string InternalServerError = "An unexpected error occurred.";

    public UserController(Mediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    #region [GET] Get User
    [HttpGet("")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerOperation(Summary = "Metodo para obtener la información de un usuario.")]
    public async Task<IActionResult> GetUserById([FromHeader] Guid userId)
    {
        try
        {
            var result = await _mediator.Send(new GetUserRequest() { UserId = userId });

            return result.Code switch
            {
                (int)Code.SUCCESS => Ok(result),
                (int)Code.BAD_REQUEST => BadRequest(result),
                (int)Code.NOT_FOUND => NotFound(result),
                (int)Code.CONFLICT => Conflict(result),
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

    #region [GET] List User By Filters
    [HttpGet("list")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerOperation(Summary = "Metodo para obtener una lista usuarios por filtro y paginado")]
    public async Task<IActionResult> ListUserByFilters([FromQuery] string fullName, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        try
        {
            var result = await _mediator.Send(new ListUsersByFiltersRequest { FullName = fullName, PageNumber = pageNumber, PageSize = pageSize });

            return result.Code switch
            {
                (int)Code.SUCCESS => Ok(result),
                (int)Code.BAD_REQUEST => BadRequest(result),
                (int)Code.NOT_FOUND => NotFound(result),
                (int)Code.CONFLICT => Conflict(result),
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

    #region [PUT] Update Agent
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerOperation(Summary = "Metodo para modificar la información de un usuario.")]
    public async Task<IActionResult> UpdateAgent([FromHeader] Guid UserId, UpdateUserRequestModel request)
    {
        try
        {
            var updateAgentRequest = _mapper.Map<UpdateUserRequest>(request);
            updateAgentRequest.Id = UserId;
            var result = await _mediator.Send(updateAgentRequest);

            return result.Code switch
            {
                (int)Code.SUCCESS => Ok(result),
                (int)Code.BAD_REQUEST => BadRequest(result),
                (int)Code.NOT_FOUND => NotFound(result),
                (int)Code.CONFLICT => Conflict(result),
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

    #region [PUT] Update User Photo
    [HttpPut("photo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerOperation(Summary = "Método para actualizar la foto de perfil del usuario.")]
    public async Task<IActionResult> UpdateUserPhoto([FromHeader] Guid UserId, [FromForm] UpdateUserPhotoRequestModel request)
    {
        try
        {
            var updatePhotoRequest = new UpdateUserPhotoRequest
            {
                UserId = UserId,
                Photo = request.Photo
            };

            var result = await _mediator.Send(updatePhotoRequest);

            return result.Code switch
            {
                (int)Code.SUCCESS => Ok(result),
                (int)Code.BAD_REQUEST => BadRequest(result),
                (int)Code.NOT_FOUND => NotFound(result),
                (int)Code.CONFLICT => Conflict(result),
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
