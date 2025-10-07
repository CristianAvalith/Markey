using Markey.CrossCutting.Excepciones;
using Markey.Domain.Interfaces;
using Markey.Domain.Models;
using Markey.Persistance.Data.Tables;
using Markey.Persistance.DTOs;
using Markey.Persistance.Interface;
using Microsoft.AspNetCore.Identity;

namespace Markey.Domain.Services;

public class LoginService : ILoginService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public LoginService(IJwtTokenGenerator jwtTokenGenerator, UserManager<User> userManager, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<TokenResponse> LoginAsync(LoginData userData)
    {
        User user = await _userRepository.GetUserByUserNameAsync(userData.UserName);
        var isValid = await _userManager.CheckPasswordAsync(user, userData.Password);
        if (!isValid) { throw new IncorrectCredentialException(); }
        return await _jwtTokenGenerator.GenerateSessionTokenAsync(user);
    }
}
