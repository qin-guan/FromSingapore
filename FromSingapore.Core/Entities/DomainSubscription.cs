namespace FromSingapore.Core.Entities;

public class DomainSubscription : Subscription
{
    public Guid DomainSubscriptionPlanId { get; set; }
    public DomainSubscriptionPlan DomainSubscriptionPlan { get; set; }
}