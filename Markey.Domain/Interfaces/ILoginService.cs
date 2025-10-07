using Markey.Domain.Models;
using Markey.Persistance.DTOs;

namespace Markey.Domain.Interfaces;
public interface ILoginService
{
    Task<TokenResponse> LoginAsync(LoginData userData);
}
