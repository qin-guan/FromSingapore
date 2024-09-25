using System.Text.Json;
using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.WebApi.Extensions;
using FromSingapore.WebApi.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.WebApi.Endpoints.Link;

public class ListLinksEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<ListLinksResponse>
{
    public override void Configure()
    {
        Get("/Link");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await dbContext.Users
            .WhereUserFromIdentity(HttpContext.User.Identity)
            .Include(u => u.Links)
            .SingleOrDefaultAsync(cancellationToken: ct);

        ArgumentNullException.ThrowIfNull(user);

        await SendAsync(new ListLinksResponse(user.Links.ToDto()), cancellation: ct);
    }
}