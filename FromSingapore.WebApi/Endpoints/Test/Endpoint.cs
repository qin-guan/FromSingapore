using FastEndpoints;
using FromSingapore.WebApi.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FromSingapore.WebApi.Endpoints.Test;

public class Endpoint : EndpointWithoutRequest<Results<Ok<TestDto>, NotFound>>
{
    public override void Configure()
    {
        Get("/Nice");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendResultAsync(TypedResults.Ok(new InheritedDto
        {
            Nice = ""
        }));
    }
}
