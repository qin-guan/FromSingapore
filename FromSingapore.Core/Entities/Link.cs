using Microsoft.EntityFrameworkCore;

namespace FromSingapore.Core.Entities;

[Index(nameof(DomainId), nameof(ShortCode), IsUnique = true)]
public class Link
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ShortCode { get; set; }
    public required Uri OriginalUri { get; set; }
    public required bool IsBanned { get; set; }
    public required bool IsActive { get; set; }

    public Guid? LinkVisitLimitId { get; set; }
    public LinkVisitLimit? LinkVisitLimit { get; set; }

    public Guid? LinkExpirationId { get; set; }
    public LinkExpiration? LinkExiration { get; set; }
    
    public Guid? LinkPasswordId { get; set; }
    public LinkPassword? LinkPassword { get; set; }

    public Guid DomainId { get; set; }
    public Domain Domain { get; set; }

    public ICollection<LinkVisit> LinkVisits = [];
}