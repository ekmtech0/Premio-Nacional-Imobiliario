using System;
using server.DTOs;
using server.Services.Interfaces;

namespace server.Endpoints;

public static class EleitorEndpoints
{
    public static void MapEleitorEndpoints(this IEndpointRouteBuilder app)
    {
        var routes = app.MapGroup("/api/eleitor")
        .WithTags("Eleitor");

        routes.MapPost("/", async (EleitorDTO eleitorDto, IEleitorService eleitorService) =>
        {
            var code = await eleitorService.CreateEleitorAsync(eleitorDto);
            return Results.Ok(new { ConfirmationCode = code });
        });

        routes.MapPost("/confirm", async (string code, IEleitorService eleitorService) =>
        {
            var result = await eleitorService.ConfirmEleitorEmailAsync(code);
            if (result)
            {
                return Results.Ok(new { Message = "Email confirmed successfully." });
            }
            return Results.BadRequest(new { Message = "Invalid confirmation code." });
        });
    }
}
