namespace FromSingapore.Core.Entities;

public class Subscription
{
    public Guid Id { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public required Guid PlanId { get; set; }
    public required string StripeSubscriptionId { get; set; }
}