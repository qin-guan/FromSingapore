using FastEndpoints;
using FromSingapore.Core.StaticStore;
using FromSingapore.WebApi.Mappers;
using Stripe;

namespace FromSingapore.WebApi.Endpoints.Plan;

public class ListPlansEndpoint : EndpointWithoutRequest<ListPlansResponse>
{
    public override void Configure()
    {
        Get("/Plan");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var tasks = StaticStore.Plans
            .Select(async (plan) =>
            {
                var price = await new PriceService().GetAsync(plan.StripePriceId, cancellationToken: ct);
                var dto = plan.ToDto();

                dto.PriceCurrency = price.Currency;
                dto.PriceAmount = (price.UnitAmount ?? 0) / 100m;

                return dto;
            });

        await SendAsync(
            new ListPlansResponse(
                await Task.WhenAll(tasks)
            ),
            cancellation: ct
        );
    }
}