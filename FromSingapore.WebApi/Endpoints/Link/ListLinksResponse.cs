using FromSingapore.WebApi.Dtos;

namespace FromSingapore.WebApi.Endpoints.Link;

public record ListLinksResponse(
    IEnumerable<LinkDto> Links
);