namespace FromSingapore.Core.Entities;

public class LinkSubscriptionPlan : SubscriptionPlan
{
    public ICollection<LinkSubscription> LinkSubscriptions { get; set; } = [];
}