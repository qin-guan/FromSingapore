namespace FromSingapore.Core.Context;

public class LinkSubscription : Subscription
{
    public Guid LinkSubscriptionPlanId { get; set; }
    public LinkSubscriptionPlan LinkSubscriptionPlan { get; set; }
}