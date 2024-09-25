using FromSingapore.WebApi.Dtos;

namespace FromSingapore.WebApi.Endpoints.Link;

public record CreateLinkRequest(
    string Title,
    string Description,
    string ShortCode,
    Uri OriginalUri,
    Guid DomainId
);