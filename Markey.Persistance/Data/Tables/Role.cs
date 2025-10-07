using Markey.Persistance.Data.Tables.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Markey.Persistance.Data.Tables;

public class Role : IdentityRole<Guid>, IEntityBase
{
    [Required]
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
