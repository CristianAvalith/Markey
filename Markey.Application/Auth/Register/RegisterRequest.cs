using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.ResponseData;
using Microsoft.AspNetCore.Http;

namespace Markey.Application.Auth.Register;

public class RegisterRequest : IRequest<ResponseData<RegisterResponse, ResponseMessage>>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public Guid OccupationId { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public IFormFile Photo { get; set; }
}
