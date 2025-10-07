using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.ResponseData;

namespace Markey.Application.User.GetUser;

public class GetUserRequest : IRequest<ResponseData<GetUserResponse, ResponseMessage>>
{
    public Guid UserId { get; set; }
}
