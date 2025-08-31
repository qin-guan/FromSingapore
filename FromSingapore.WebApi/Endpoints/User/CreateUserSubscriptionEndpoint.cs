using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.Core.Entities;
using FromSingapore.Core.StaticStore;
using FromSingapore.WebApi.Extensions;
using FromSingapore.WebApi.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace FromSingapore.WebApi.Endpoints.User;

public class CreateUserSubscriptionsEndpoint(AppDbContext dbContext, IOptions<StripeOptions> stripeOptions)
    : EndpointWithoutRequest<CreateUserSubscriptionResponse>
{
    public override void Configure()
    {
        Post("/User/{UserId}/Subscription/{PlanId}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var planId = Route<Guid>("PlanId");
        var plan = StaticStore.Plans.SingleOrDefault(p => p.Id == planId);
        ArgumentNullException.ThrowIfNull(plan);

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

        if (subscription is not null)
        {
            await SendErrorsAsync(400, ct);
            return;
        }

        var customer = await dbContext.AppUserStripeCustomers
            .SingleOrDefaultAsync(s => s.AppUserId == user.Id, cancellationToken: ct);

        if (customer is null)
        {
            var c = await new CustomerService().CreateAsync(
                new CustomerCreateOptions
                {
                    Email = user.Email,
                },
                cancellationToken: ct
            );

            customer = (await dbContext.AppUserStripeCustomers.AddAsync(
                new AppUserStripeCustomer
                {
                    StripeCustomerId = c.Id,
                    AppUserId = user.Id
                }, ct
            )).Entity;

            await dbContext.SaveChangesAsync(ct);
        }

        var link = await new SessionService().CreateAsync(
            new SessionCreateOptions
            {
                SuccessUrl = $"{stripeOptions.Value.ApiUri}/success.html?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{stripeOptions.Value.ApiUri}/canceled.html",
                Mode = "subscription",
                Customer = customer.StripeCustomerId,
                LineItems =
                [
                    new SessionLineItemOptions
                    {
                        Price = plan.StripePriceId,
                        Quantity = 1
                    }
                ]
            }, cancellationToken: ct
        );

        await dbContext.SaveChangesAsync(ct);
        
        await SendAsync(new CreateUserSubscriptionResponse(link.Url), cancellation: ct);
    }
}