namespace FromSingapore.Core.Entities;

public class DomainSubscriptionPlan : SubscriptionPlan
{
    public ICollection<DomainSubscription> DomainSubscriptions { get; set; } = [];
}