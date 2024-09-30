using FromSingapore.WebApi.Dtos;

namespace FromSingapore.WebApi.Endpoints.Plan;

public record ListPlansResponse(
    IEnumerable<PlanDto> Plans
);