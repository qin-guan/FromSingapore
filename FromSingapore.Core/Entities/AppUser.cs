using Microsoft.AspNetCore.Identity;

namespace FromSingapore.Core.Entities;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<Link> Links { get; set; } = [];
}