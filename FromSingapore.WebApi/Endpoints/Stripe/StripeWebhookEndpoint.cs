using System.Text.Json;
using FastEndpoints;
using FromSingapore.Core.Context;
using FromSingapore.Core.StaticStore;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Subscription = FromSingapore.Core.Entities.Subscription;

namespace FromSingapore.WebApi.Endpoints.Stripe;

public class StripeWebhookEndpoint(ILogger<StripeWebhookEndpoint> logger, AppDbContext dbContext)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/Stripe/Webhook");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(ct);

        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);

            switch (stripeEvent.Type)
            {
                case "invoice.payment_succeeded":
                {
                    var e = await new EventService().GetAsync(stripeEvent.Id, cancellationToken: ct);
                    if (e.Data.Object is not Invoice invoice)
                    {
                        throw new Exception("Invoice not handled.");
                    }

                    var subscription =
                        await new SubscriptionService().GetAsync(invoice.SubscriptionId, cancellationToken: ct);

                    if (subscription.Status != "active")
                    {
                        logger.LogWarning("Payment subscription event received but subscription is not active");
                        break;
                    }

                    var customer = await dbContext.AppUserStripeCustomers.SingleOrDefaultAsync(u =>
                        u.StripeCustomerId == subscription.CustomerId, cancellationToken: ct);

                    ArgumentNullException.ThrowIfNull(customer);

                    var plan = StaticStore.Plans.FirstOrDefault(p =>
                        p.StripeProductId == subscription.Items.First().Plan.ProductId
                    );

                    ArgumentNullException.ThrowIfNull(plan);

                    await dbContext.Subscriptions.AddAsync(new Subscription
                    {
                        AppUserId = customer.AppUserId,
                        PlanId = plan.Id,
                        StripeSubscriptionId = subscription.Id
                    }, ct);

                    await dbContext.SaveChangesAsync(ct);

                    break;
                }
                default:
                {
                    logger.LogWarning("Unhandled Stripe Webhook event: {Event}", stripeEvent);
                    break;
                }
            }

            await SendOkAsync(ct);
        }
        catch (StripeException ex)
        {
            logger.LogCritical("Exception when handling Stripe Webhook: {Ex}", ex);
            await SendErrorsAsync(cancellation: ct);
        }
    }
}