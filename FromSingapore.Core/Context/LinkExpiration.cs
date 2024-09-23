using Microsoft.EntityFrameworkCore;

namespace FromSingapore.Core.Context;

public class LinkExpiration
{
    public Guid Id { get; set; }

    public required DateTime ExpiresAt { get; set; }
}