using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.ResponseData;
using Markey.Persistance.DTOs;
namespace Markey.Application.User.Update;

public class UpdateUserRequest : IRequest<ResponseData<UpdateUserResponse, ResponseMessage>>
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public Guid OccupationId { get; set; }
    public string PhoneNumber { get; set; }
}
