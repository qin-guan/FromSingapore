using System.ComponentModel.DataAnnotations;

namespace FromSingapore.Core.Context;

public class LinkPassword
{
    public Guid Id { get; set; }
    
    [MinLength(1)]
    public required string Password { get; set; }
}