using Markey.Persistance.DTOs;
namespace Markey.Application.User.List.ListUsers;

public class ListUsersByFiltersResponse
{
    public List<ResumeUserData> Users { get; set; }
    public int Count { get; set; }
    public int PageNumber { get; set; }
}
