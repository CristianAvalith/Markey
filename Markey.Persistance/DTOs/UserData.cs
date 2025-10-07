using Microsoft.AspNetCore.Http;
using System;
namespace Markey.Persistance.DTOs;
public class UserData
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public Guid OccupationId { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile Photo { get; set; }
}
