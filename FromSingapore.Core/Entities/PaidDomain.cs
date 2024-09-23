namespace FromSingapore.Core.Entities;

public class PaidDomain : Domain
{
    public Guid DomainSubscriptionId { get; set; }
    public DomainSubscription DomainSubscription { get; set; }
}