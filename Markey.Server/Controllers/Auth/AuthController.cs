using AutoMapper;
using Markey.Application.Auth.Login;
using Markey.Application.Auth.Register;
using Markey.CrossCutting.Helpers;
using Markey.CrossCutting.Mediator;
using Markey.Server.Controllers.Auth.Request;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Markey.Server.Controllers.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly Mediator _mediator;
        private readonly IMapper _mapper;
        private readonly string _error = "An unexpected error occurred.";

        public AuthController(Mediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #region [POST] Register
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Método de Creación de Usuario con foto")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestModel requestModel)
        {
            try
            {
                RegisterRequest request = _mapper.Map<RegisterRequest>(requestModel);
                request.Photo = requestModel.Photo;
                var result = await _mediator.Send(request);

                return result.Code switch
                {
                    (int)Code.CREATED => Created("", result),
                    (int)Code.BAD_REQUEST => BadRequest(result),
                    (int)Code.CONFLICT => Conflict(result),
                    (int)Code.SERVICE_UNAVAILABLE => StatusCode(StatusCodes.Status503ServiceUnavailable, result),
                    _ => StatusCode(StatusCodes.Status503ServiceUnavailable, result)
                };
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = _error });
            }
        }
        #endregion


        #region [POST] Login
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Metodo de Autenticación de Usuario")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<LoginRequest>(request));

                return result.Code switch
                {
                    (int)Code.SUCCESS => Ok(result),
                    (int)Code.UNAUTHORIZED => Unauthorized(result),
                    (int)Code.NOT_FOUND => NotFound(result),
                    (int)Code.SERVICE_UNAVAILABLE => StatusCode(StatusCodes.Status503ServiceUnavailable, result),
                    _ => StatusCode(StatusCodes.Status503ServiceUnavailable, result)
                };
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = _error });
            }
        }
        #endregion

    }
}
