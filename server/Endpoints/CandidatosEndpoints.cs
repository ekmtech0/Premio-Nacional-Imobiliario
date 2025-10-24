using server.DTOs;
using server.Services.Interfaces;

namespace server.Endpoints
{
    public static class CandidatosEndpoints
    {
        public static void MapCandidatoEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/api/candidatos")
                .WithDescription("Endpoints para gerenciar candidatos")
                .WithTags("Candidatos");

            endpoints.MapGet("/candidatos", async (ICandidatoService service) =>
            {
                var candidatos = await service.GetAllCandidatosAsync();
                return Results.Ok(candidatos);
            });
            endpoints.MapGet("/candidatos/{id}", async (Guid id, ICandidatoService service) =>
            {
                var candidato = await service.GetCandidatoByIdAsync(id);
                return candidato is not null ? Results.Ok(candidato) : Results.NotFound();
            });
            endpoints.MapPost("/candidatos", async (CandidatoDTO model, ICandidatoService service) =>
            {
                var candidato = await service.CadastrarCandidatoAsync(model);
                return Results.Created($"/candidatos/{candidato.Nome}", candidato);
            });
            endpoints.MapDelete("/candidatos/{id}", async (Guid id, ICandidatoService service) =>
            {
                var deleted = await service.DeleteCandidatoAsycnc(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
