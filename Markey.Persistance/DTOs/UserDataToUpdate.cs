using System;
namespace Markey.Persistance.DTOs;

public class UserDataToUpdate
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public Guid OccupationId { get; set; }
    public string PhoneNumber { get; set; }
}
