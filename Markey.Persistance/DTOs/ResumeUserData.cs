namespace Markey.Persistance.DTOs;
public class ResumeUserData
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string PhotoUrl { get; set; }    
    public OccupationData Occupation { get; set; }
}
