using FromSingapore.WebApi.Dtos;

namespace FromSingapore.WebApi.Endpoints.User;

public record GetUserSubscriptionsResponse(
    SubscriptionDto? Subscription
);