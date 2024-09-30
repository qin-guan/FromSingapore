using System.ComponentModel.DataAnnotations;

namespace FromSingapore.Core.Entities;

public class AppUserStripeCustomer
{
    [Key] public required string StripeCustomerId { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}