using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.WebApi.Events.LinkVisited;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.WebApi.Endpoints.Slug;

public class RedirectEndpoint(AppDbContext dbContext) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/{ShortCode}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var shortCode = Route<string>("ShortCode");

        var link = await dbContext.Links
            .Include(l => l.LinkExpiration)
            .Include(l => l.LinkPassword)
            .Include(l => l.LinkVisitLimit)
            .Include(l => l.LinkVisits)
            .SingleOrDefaultAsync(l => l.ShortCode == shortCode, ct);

        await SendRedirectAsync(
            link is null ? "https://www.from.sg" : link.OriginalUri.ToString(),
            allowRemoteRedirects: true
        );
    }
    
    public override async Task OnAfterHandleAsync(EmptyRequest req, object? res, CancellationToken ct)
    {
        var shortCode = Route<string>("ShortCode");
        var link = await dbContext.Links
            .SingleOrDefaultAsync(l => l.ShortCode == shortCode, ct);

        ArgumentNullException.ThrowIfNull(link);

        await PublishAsync(
            new LinkVisitedEvent
            {
                Id = link.Id
            },
            cancellation: ct
        );
    }
}