using System.Text.Json;
using FastEndpoints;
using FromSingapore.Core.Context;
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
        await SendStringAsync(JsonSerializer.Serialize(HttpContext.User.Identities), cancellation: ct);
    }
}