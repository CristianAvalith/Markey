using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.ResponseData;
using Microsoft.AspNetCore.Http;

namespace Markey.Application.User.UpdatePhoto;

public class UpdateUserPhotoRequest : IRequest<ResponseData<UpdateUserPhotoResponse, ResponseMessage>>
{
    public Guid UserId { get; set; }
    public IFormFile Photo { get; set; }
}