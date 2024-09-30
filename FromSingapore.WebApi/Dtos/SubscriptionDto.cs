using Riok.Mapperly.Abstractions;

namespace FromSingapore.WebApi.Dtos;

public record SubscriptionDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string StripeProductId { get; set; }
    public string StripePriceId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

    /// <summary>
    /// Populate from Plan manually
    /// </summary>
    [MapperIgnore]
    public PlanFeaturesDto Features { get; set; }

    /// <summary>
    /// Populate from Stripe APi
    /// </summary>
    [MapperIgnore]
    public string PriceCurrency { get; set; }

    /// <summary>
    /// Populate from Stripe APi
    /// </summary>
    [MapperIgnore]
    public decimal PriceAmount { get; set; }

    public record PlanFeaturesDto(
        int DomainsAvailable,
        int DomainsRemaining
    );
}