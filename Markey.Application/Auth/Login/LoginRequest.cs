using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.ResponseData;
namespace Markey.Application.Auth.Login;

public class LoginRequest : IRequest<ResponseData<LoginResponse, ResponseMessage>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
