namespace FromSingapore.Core.Context;

public class PaidDomain : Domain
{
    public Guid DomainSubscriptionId { get; set; }
    public DomainSubscription DomainSubscription { get; set; }
}