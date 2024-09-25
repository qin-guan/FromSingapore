using FromSingapore.WebApi.Dtos;

namespace FromSingapore.WebApi.Endpoints.Domain;

public record ListDomainsResponse(
    IEnumerable<DomainDto> Domains
);