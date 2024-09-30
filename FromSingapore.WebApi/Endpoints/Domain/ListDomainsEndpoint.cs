using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.Core.Entities;
using FromSingapore.WebApi.Extensions;
using FromSingapore.WebApi.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.WebApi.Endpoints.Domain;

public class ListDomainsEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<ListDomainsResponse>
{
    public override void Configure()
    {
        Get("/Domain");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await dbContext.Users
            .WhereUserFromIdentity(HttpContext.User.Identity)
            .SingleOrDefaultAsync(cancellationToken: ct);

        ArgumentNullException.ThrowIfNull(user);

        var domains = await dbContext.Domains
            .Include(d => ((PaidDomain)d).Subscription)
            .Where(d => d is FreeDomain || (d is PaidDomain && ((PaidDomain)d).Subscription.AppUserId == user.Id))
            .ToListAsync(cancellationToken: ct);

        await SendAsync(new ListDomainsResponse(domains.ToDto()), cancellation: ct);
    }
}