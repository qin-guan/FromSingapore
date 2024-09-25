namespace FromSingapore.Core.Entities;

public abstract class Subscription
{
    public Guid Id { get; set; }

    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public bool IsValid => DateTime.Now >= StartDate && DateTime.Now <= EndDate;

    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}