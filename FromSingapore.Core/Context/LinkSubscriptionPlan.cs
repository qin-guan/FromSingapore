namespace FromSingapore.Core.Context;

public class LinkSubscriptionPlan : SubscriptionPlan
{
    public ICollection<LinkSubscription> LinkSubscriptions = [];
}