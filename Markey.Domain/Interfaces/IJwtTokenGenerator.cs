using Markey.Domain.Models;
using Markey.Persistance.Data.Tables;
using System;
namespace Markey.Domain.Interfaces;
public interface IJwtTokenGenerator
{
    Task<TokenResponse> GenerateSessionTokenAsync(User user);
}