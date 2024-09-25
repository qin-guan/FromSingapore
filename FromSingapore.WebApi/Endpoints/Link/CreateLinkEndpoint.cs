using System.Text.Json;
using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.Core.Entities;
using FromSingapore.WebApi.Dtos;
using FromSingapore.WebApi.Extensions;
using FromSingapore.WebApi.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.WebApi.Endpoints.Link;

public class CreateLinksEndpoint(AppDbContext dbContext) : Endpoint<CreateLinkRequest, LinkDto>
{
    public override void Configure()
    {
        Post("/Link");
    }

    public override async Task HandleAsync(CreateLinkRequest req, CancellationToken ct)
    {
        var user = await dbContext.Users
            .WhereUserFromIdentity(HttpContext.User.Identity)
            .SingleOrDefaultAsync(ct);

        ArgumentNullException.ThrowIfNull(user);

        var domain = await dbContext.Domains
            .Include(paidDomain => ((PaidDomain)paidDomain).DomainSubscription)
            .SingleOrDefaultAsync(d => d.Id == req.DomainId, ct);

        ArgumentNullException.ThrowIfNull(domain);

        if (domain is PaidDomain pd)
        {
            var subscriberIsUser = pd.DomainSubscription.AppUserId == user.Id;

            if (!subscriberIsUser || !pd.DomainSubscription.IsValid)
            {
                await SendForbiddenAsync(ct);
                return;
            }
        }

        var link = await dbContext.Links.AddAsync(new Core.Entities.Link
        {
            Title = req.Title,
            Description = req.Description,
            ShortCode = req.ShortCode,
            OriginalUri = req.OriginalUri,
            IsBanned = false,
            IsDisabled = false,
            DomainId = domain.Id,
            AppUserId = user.Id
        }, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendAsync(link.Entity.ToDto(), cancellation: ct);
    }
}