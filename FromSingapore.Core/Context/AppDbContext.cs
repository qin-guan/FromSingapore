using FromSingapore.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.Core.Context;

public partial class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    // Domains
    public DbSet<Domain> Domains { get; set; }
    public DbSet<FreeDomain> FreeDomains { get; set; }
    public DbSet<PaidDomain> PaidDomains { get; set; }

    // Links
    public DbSet<Link> Links { get; set; }
    public DbSet<LinkVisit> LinkVisits { get; set; }
    
    // Link configurations
    public DbSet<LinkExpiration> LinkExpirations { get; set; }
    public DbSet<LinkVisitLimit> LinkVisitLimits { get; set; }
    public DbSet<LinkPassword> LinkPasswords { get; set; }

    // Subscriptions
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<DomainSubscription> DomainSubscriptions { get; set; }
    public DbSet<DomainSubscriptionPlan> DomainSubscriptionPlans { get; set; }
    public DbSet<LinkSubscription> LinkSubscriptions { get; set; }
    public DbSet<LinkSubscriptionPlan> LinkSubscriptionPlans { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain>()
            .UseTptMappingStrategy();

        modelBuilder.Entity<Subscription>()
            .UseTptMappingStrategy();

        modelBuilder.Entity<SubscriptionPlan>()
            .UseTptMappingStrategy();

        var fromSgDomain = new FreeDomain
        {
            Id = Guid.Parse("04569f15-7a77-4cf3-8477-6b37a1c1fe01"),
            Name = "from.sg",
            Description = "Free domain"
        };

        var normalLink = new Link
        {
            Id = Guid.Parse("8d3e3c94-a972-4a60-9f7f-def1ef1303aa"),
            Title = "Normal link",
            Description = "Normal link without any special restrictions",
            ShortCode = "qg",
            OriginalUri = new Uri("https://qinguan.me"),
            IsBanned = false,
            IsDisabled = false,
            DomainId = fromSgDomain.Id
        };

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}