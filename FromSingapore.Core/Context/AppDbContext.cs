using FromSingapore.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.Core.Context;

public partial class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<AppUserStripeCustomer> AppUserStripeCustomers { get; set; }

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

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain>()
            .UseTptMappingStrategy();

        var fromSgDomain = new FreeDomain
        {
            Id = Guid.Parse("04569f15-7a77-4cf3-8477-6b37a1c1fe01"),
            Name = "from.sg",
            Description = "Free domain"
        };

        modelBuilder.Entity<FreeDomain>()
            .HasData(fromSgDomain);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}