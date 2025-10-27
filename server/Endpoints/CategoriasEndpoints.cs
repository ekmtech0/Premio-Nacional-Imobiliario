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
            endpoints.MapPost("/", async (CategoriaCreateDTO model, ICategoriaService service) =>
            {
                var categoria = await service.AddCategoriaAsnyc(model);
                return categoria is not null ? Results.Created($"/candidatos/{categoria.Nome}", categoria) : Results.Content("Categoria existente");
            });
            endpoints.MapDelete("/{id}", async (Guid id, ICategoriaService service) =>
            {
                var deleted = await service.DeleteCategoriaAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
            endpoints.MapPut("/{id}", async (Guid id, CategoriaCreateDTO model, ICategoriaService service) =>
            {
                var categoria = await service.UpdateCategoriaAsync(id, model);
                return categoria is not null ? Results.Ok(categoria) : Results.NotFound();
            });
            endpoints.MapGet("/no-user", async (ICategoriaService service) =>
            {
                var categorias = await service.GetCategoriaWithNoAsync();
                return Results.Ok(categorias);
            });
        }
    }
}
