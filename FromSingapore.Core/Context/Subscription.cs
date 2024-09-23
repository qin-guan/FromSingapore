namespace FromSingapore.Core.Context;

public abstract class Subscription
{
    public Guid Id { get; set; }

    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}