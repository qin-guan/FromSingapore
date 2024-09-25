using FromSingapore.Core.Entities;
using FromSingapore.WebApi.Dtos;
using Riok.Mapperly.Abstractions;

namespace FromSingapore.WebApi.Mappers;

[Mapper]
public static partial class DomainMapper
{
    public static partial DomainDto ToDto(this Domain domain);
    public static partial IEnumerable<DomainDto> ToDto(this IEnumerable<Domain> domains);
}