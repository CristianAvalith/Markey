using System;
using System.ComponentModel.DataAnnotations;


namespace Markey.Persistance.Data.Tables.Base;

public class EntityBase
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Description { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
