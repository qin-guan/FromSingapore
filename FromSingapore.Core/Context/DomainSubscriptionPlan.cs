namespace FromSingapore.Core.Context;

public class DomainSubscriptionPlan : SubscriptionPlan
{
    public ICollection<DomainSubscription> DomainSubscriptions = [];
}