namespace FromSingapore.Core.Entities;

public class LinkExpiration
{
    public Guid Id { get; set; }

    public required DateTime ExpiresAt { get; set; }
}