using server.DTOs;
using server.Services.Interfaces;

namespace server.Endpoints
{
    public static class VotoEndpoints
    {
        public static void MapVotoEndpoints(this IEndpointRouteBuilder app)
        {
            var routes = app.MapGroup("/api/votos")
                    .WithTags("Votos");

            routes.MapPost("/", async (IVotoService service, VotoCreateDTO model) =>
            {
                var voto = await service.CreateVotoAsync(model);
                return voto is null ? Results.BadRequest("Não pode mais votar nesta categoria") : Results.Created($"/votos/{voto.Dados?.Id}", voto);
            });
            routes.MapGet("/", async (IVotoService service) =>
            {
                var votos = await service.GetAllVotosAsync();
                return Results.Ok(votos);
            });
            routes.MapGet("/categoria/{id:guid}", async (IVotoService service, Guid id) =>
            {
                var votos = await service.GetVotosByCategoriaAsync(id);
                return Results.Ok(votos);
            });
            routes.MapGet("/total/categoria/{id:guid}", async (IVotoService service, Guid id) =>
            {
                var total = await service.TotalVotosCategoriAsync(id);
                return Results.Ok(total);
            });
            routes.MapGet("/total", async (IVotoService service) =>
            {
                var total = await service.TotalVotosGeralAsync();
                return Results.Ok(total);
            });
        }
    }
}
