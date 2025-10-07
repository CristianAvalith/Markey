using System;

namespace Markey.Domain.Models;

public class TokenInformation
{
    public Guid UserId { get; set; }
    public DateTime ExpirationDate { get; set; }
}
