namespace FromSingapore.WebApi.Dtos;

public record LinkDto(
    Guid Id,
    string Title,
    string Description,
    string ShortCode,
    Uri OriginalUri,
    Guid DomainId,
    Guid? LinkVisitLimitId,
    Guid? LinkExpirationId,
    Guid? LinkPasswordId
);