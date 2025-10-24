using server.DTOs;
using server.Services.Interfaces;

namespace server.Endpoints
{
    public static class CategoriasEndpoints
    {
        public static void MapCategoriaEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/api/categorias")
                .WithDescription("Endpoints para gerenciar categorias")
                .WithTags("Categorias");
            // Define os enpoints aqui usando ICategoriaService

            endpoints.MapGet("/", async (ICategoriaService service) =>
            {
                var Categorias = await service.GetAllCategoriasAsync();
                return Results.Ok(Categorias);
            });
            endpoints.MapGet("/{id}", async (Guid id, ICategoriaService service) =>
            {
                var categoria = await service.GetCategoriaByIdAsync(id);
                return categoria is not null ? Results.Ok(categoria) : Results.NotFound();
            });
            endpoints.MapPost("/", async (CategoriaDTO model, ICategoriaService service) =>
            {
                var categoria = await service.AddCategoriaAsnyc(model);
                return Results.Created($"/candidatos/{categoria.Nome}", categoria);
            });
            endpoints.MapDelete("/{id}", async (Guid id, ICategoriaService service) =>
            {
                var deleted = await service.DeleteCategoriaAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
