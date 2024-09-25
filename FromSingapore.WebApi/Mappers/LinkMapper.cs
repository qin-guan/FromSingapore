using FromSingapore.Core.Entities;
using FromSingapore.WebApi.Dtos;
using Riok.Mapperly.Abstractions;

namespace FromSingapore.WebApi.Mappers;

[Mapper]
public static partial class LinkMapper
{
    public static partial LinkDto ToDto(this Link link);
    public static partial IEnumerable<LinkDto> ToDto(this IEnumerable<Link> links);
}