namespace FromSingapore.Core.StaticStore.Plans;

public abstract record Plan
{
    public Guid Id { get; protected init; }
    public string Name { get; protected init; }
    public string StripeProductId { get; protected init; }
    public string StripePriceId { get; protected init; }
    public bool Disabled { get; protected init; }

    public PlanFeatures Features { get; protected init; }

    public record PlanFeatures(
        int DomainsAvailable,
        bool LinkPassword,
        bool LinkVisitLimit
    );
};