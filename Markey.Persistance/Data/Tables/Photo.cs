using Markey.Persistance.Data.Tables.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Markey.Persistance.Data.Tables;

public class Photo : EntityBaseGuid
{

    [Required]
    public string Url { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public User User { get; set; }
}

