namespace FromSingapore.Core.Entities;

public class PaidDomain : Domain
{
    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
}