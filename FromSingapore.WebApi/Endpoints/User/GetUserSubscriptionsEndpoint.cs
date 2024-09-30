using System.Text.Json;
using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.Core.StaticStore;
using FromSingapore.WebApi.Dtos;
using FromSingapore.WebApi.Extensions;
using FromSingapore.WebApi.Mappers;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace FromSingapore.WebApi.Endpoints.User;

public class GetUserSubscriptionsEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<GetUserSubscriptionsResponse>
{
    public override void Configure()
    {
        Get("/User/{UserId}/Subscription");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("UserId");
        var user = await dbContext.Users.WhereUserFromIdentity(HttpContext.User.Identity).SingleOrDefaultAsync(ct);
        ArgumentNullException.ThrowIfNull(user);

        if (user.Id != userId)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var subscription = await dbContext.Subscriptions
            .SingleOrDefaultAsync(s => s.AppUserId == user.Id, ct);

        if (subscription is null)
        {
            await SendAsync(new GetUserSubscriptionsResponse(null), cancellation: ct);
            return;
        }

        var plan = StaticStore.Plans.Single(p => p.Id == subscription.PlanId);
        var dto = plan.ToSubscriptionDto();

        var price = await new PriceService().GetAsync(plan.StripePriceId, cancellationToken: ct);

        dto.PriceCurrency = price.Currency;
        dto.PriceAmount = (price.UnitAmount ?? 0) / 100m;

        var stripeSubscription =
            await new SubscriptionService().GetAsync(subscription.StripeSubscriptionId, cancellationToken: ct);

        dto.Features = new SubscriptionDto.PlanFeaturesDto(
            plan.Features.DomainsAvailable,
            await dbContext.PaidDomains.CountAsync(d => d.SubscriptionId == subscription.Id, ct)
        );

        dto.StartDate = stripeSubscription.CurrentPeriodStart;
        dto.EndDate = stripeSubscription.CurrentPeriodEnd;

        await SendAsync(new GetUserSubscriptionsResponse(dto), cancellation: ct);
    }
}