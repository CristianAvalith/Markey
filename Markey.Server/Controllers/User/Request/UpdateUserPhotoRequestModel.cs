using Microsoft.AspNetCore.Http;

namespace Markey.Server.Controllers.User.Request;

public class UpdateUserPhotoRequestModel
{
    public IFormFile Photo { get; set; }
}