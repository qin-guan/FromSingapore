using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.Core.Entities;

namespace FromSingapore.WebApi.Events.LinkVisited;

public class LinkVisitedEventHandler(IServiceScopeFactory serviceScopeFactory) : IEventHandler<LinkVisitedEvent>
{
    public async Task HandleAsync(LinkVisitedEvent eventModel, CancellationToken ct)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.LinkVisits.AddAsync(
            new LinkVisit
            {
                LinkId = eventModel.Id,
                VisitedAt = DateTime.UtcNow
            },
            ct
        );

        await dbContext.SaveChangesAsync(ct);
    }
}