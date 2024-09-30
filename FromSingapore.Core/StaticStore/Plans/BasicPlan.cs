namespace FromSingapore.Core.StaticStore.Plans;

public record BasicPlan : Plan
{
    public BasicPlan()
    {
        Id = Guid.Parse("1c3f5db0-8139-4f18-8b95-b7b52123f90f");

        Name = "Basic (Monthly)";
        StripeProductId = "prod_QvBmJ8K4cjBxVT";
        StripePriceId = "price_1Q3LOVRO0gSKn7efkI6qdPC6";
        Disabled = false;

        Features = new PlanFeatures(
            DomainsAvailable: 1,
            LinkPassword: true,
            LinkVisitLimit: true
        );
    }
};