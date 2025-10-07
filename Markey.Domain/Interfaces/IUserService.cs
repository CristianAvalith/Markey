
using Markey.Persistance.DTOs;
using Microsoft.AspNetCore.Http;

namespace Markey.Domain.Interfaces;

public interface IUserService
{
    Task<Guid> RegisterAsync(UserData user);
    Task<GetUserData> GetUserByIdAsync(Guid id);
    Task<List<ResumeUserData>> ListUsersByFilterAsync(string fullName, int pageSize, int pageNumber);
    Task<GetUserData> PatchUserAsync(UserDataToUpdate userToUpdate);
    Task<string> UpdateUserPhotoAsync(Guid userId, IFormFile photo);
}
