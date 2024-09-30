using FastEndpoints;
using FromSingapore.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.WebApi.Endpoints.Domain;

public class QueryDomainsEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<QueryDomainsResponse>
{
    public override void Configure()
    {
        Get("/Domain/{Name}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var name = Route<string>("Name");
        ArgumentNullException.ThrowIfNull(name);

        if (name.Contains('.'))
        {
            await SendAsync(new QueryDomainsResponse(false), cancellation: ct);
            return;
        }
        
        if (name.Any(n => !char.IsLetterOrDigit(n)))
        {
            await SendAsync(new QueryDomainsResponse(false), cancellation: ct);
            return;
        }

        var available = await dbContext.Domains.AllAsync(d => d.Name != name, ct);
        await SendAsync(new QueryDomainsResponse(available), cancellation: ct);
    }
}