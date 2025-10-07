using Markey.Persistance.DTOs;

namespace Markey.Application.User.GetUser;
public class GetUserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public OccupationData Occupation { get; set; }
    public string PhoneNumber { get; set; }
    public string PhotoUrl { get; set; }
}
