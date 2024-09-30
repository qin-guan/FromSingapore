using FromSingapore.WebApi.Dtos;
using Riok.Mapperly.Abstractions;
using Plan = FromSingapore.Core.StaticStore.Plans.Plan;

namespace FromSingapore.WebApi.Mappers;

[Mapper]
public static partial class PlanMapper
{
    public static partial PlanDto ToDto(this Plan plan);
}