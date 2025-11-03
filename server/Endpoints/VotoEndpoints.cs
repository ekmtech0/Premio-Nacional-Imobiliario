using Microsoft.AspNetCore.SignalR;
using server.DTOs;
using server.Hubs;
using server.Services.Interfaces;

namespace server.Endpoints
{
    public static class VotoEndpoints
    {
        public static void MapVotoEndpoints(this IEndpointRouteBuilder app)
        {
            var routes = app.MapGroup("/api/votos")
                    .WithTags("Votos");

            routes.MapPost("/", async (IVotoService service, ICandidatoService Cservice ,IHubContext<VotoHub> hub, VotoCreateDTO model) =>
            {
                var voto = await service.CreateVotoAsync(model);
                var qtdVotos = await service.TotalVotosGeralAsync();
                var ListMaisVotados = await Cservice.GetListaDosMaisVotadosDTOAsync();
                await hub.Clients.All.SendAsync("ReceiveVoteQtdVotos", qtdVotos);
                await hub.Clients.All.SendAsync("ReceiveListaMaisVotados", ListMaisVotados);

                return voto is null ? Results.BadRequest("Não pode mais votar nesta categoria") : Results.Created($"/votos/{voto.Dados?.Id}", voto);
            });
            routes.MapGet("/", async (IVotoService service) =>
            {
                var votos = await service.GetAllVotosAsync();
                return Results.Ok(votos);
            }).RequireAuthorization();
            routes.MapGet("/categoria/{id:guid}", async (IVotoService service, Guid id) =>
            {
                var votos = await service.GetVotosByCategoriaAsync(id);
                return Results.Ok(votos);
            }).RequireAuthorization();
            routes.MapGet("/total/categoria/{id:guid}", async (IVotoService service, Guid id) =>
            {
                var total = await service.TotalVotosCategoriAsync(id);
                return Results.Ok(total);
            }).RequireAuthorization();
            routes.MapGet("/total", async (IVotoService service) =>
            {
                var total = await service.TotalVotosGeralAsync();
                return Results.Ok(total);
            }).RequireAuthorization();
            routes.MapGet("/total/categorias", async (ICategoriaService service) =>
            {
                var votosPorCategorias = await service.GetVotosPorCategoriasAsync();
                return Results.Ok(votosPorCategorias);
            }).RequireAuthorization();
        }
    }
}
