namespace FromSingapore.WebApi.Extensions;

public sealed record IdentityInfoResponse(
    Guid Id,
    string Email,
    bool IsEmailConfirmed
);