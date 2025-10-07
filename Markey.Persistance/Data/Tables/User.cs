using Markey.Persistance.Data.Tables.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Markey.Persistance.Data.Tables;

[Table("User")]
public class User : IdentityUser<Guid>
{
    [Required]
    public string FullName { get; set; }

    [ForeignKey("Occupation")]
    public Guid OccupationId { get; set; }
    public Occupation Occupation { get; set; }
    public string PhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
