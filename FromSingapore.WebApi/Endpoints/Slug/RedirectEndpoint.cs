using System.Text;
using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.WebApi.Events.LinkVisited;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.WebApi.Endpoints.Slug;

public class RedirectEndpoint(AppDbContext dbContext, IWebHostEnvironment environment) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/{ShortCode}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var shortCode = Route<string>("ShortCode");
        var host = HttpContext.Request.Host.Host;

        if (environment.IsDevelopment() && host == "localhost")
        {
            // Fake host during development
            host = "from.sg";
        }

        var link = await dbContext.Links
            .Include(l => l.Domain)
            .Include(l => l.LinkExpiration)
            .Include(l => l.LinkPassword)
            .Include(l => l.LinkVisitLimit)
            .SingleOrDefaultAsync(l => l.ShortCode == shortCode && l.Domain.Name == host, ct);

        if (link is null)
        {
            await SendRedirectAsync("https://www.from.sg", allowRemoteRedirects: true);
            return;
        }

        if (DateTime.UtcNow > link.LinkExpiration?.ExpiresAt)
        {
            await SendRedirectAsync("https://www.from.sg", allowRemoteRedirects: true);
            return;
        }

        if (link.LinkVisitLimit is not null)
        {
            var linkVisitCount = await dbContext.LinkVisits.CountAsync(v => v.LinkId == link.Id, cancellationToken: ct);
            if (linkVisitCount >= link.LinkVisitLimit.Max)
            {
                await SendRedirectAsync("https://www.from.sg", allowRemoteRedirects: true);
                return;
            }
        }

        if (link.LinkPassword is not null)
        {
            var requestHasPassword = HttpContext.Request.Headers.TryGetValue("Authorization", out var header);
            if (!requestHasPassword)
            {
                await SendHeadersAsync((headers) => { headers.WWWAuthenticate = "Basic"; }, 401, ct);
                return;
            }

            var password = Encoding.UTF8.GetString(
                    Convert.FromBase64String(header.Last()?.Replace("Basic ", "") ?? "")
                )
                .Split(":")
                .Last();

            await SendRedirectAsync(
                password == link.LinkPassword.Password
                    ? link.OriginalUri.ToString()
                    : "https://www.from.sg",
                allowRemoteRedirects: true
            );
        }

        await PublishAsync(
            new LinkVisitedEvent
            {
                Id = link.Id
            },
            Mode.WaitForNone,
            cancellation: ct
        );

        await SendRedirectAsync(link.OriginalUri.ToString(), allowRemoteRedirects: true);
    }
}