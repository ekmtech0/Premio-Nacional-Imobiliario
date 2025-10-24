using Microsoft.EntityFrameworkCore;
using server.ApplicationDbContext;
using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext context;

        public CategoriaRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<CategoriaDTO> AddCategoriaAsnyc(CategoriaCreateDTO model)
        {
            var categoria = new Categoria
            {
                Nome = model.Nome,
                Description = model.Description
            };
           var c = await context.Categorias.AddAsync(categoria);

            return new CategoriaDTO
            {
                Id = c.Entity.Id,
                Nome = categoria.Nome,
                Description = categoria.Description,
            };
        }

        public async Task<bool> CategoriaExistsAsync(string nome) => 
            await context.Categorias
            .AnyAsync(c => c.Nome.ToLower() == nome.ToLower());

        public async Task<bool> DeleteCategoriaAsync(Guid id)
        {
            await context.Categorias
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return true;
        }

        public async Task<List<CategoriaReturnDTO>> GetAllCategoriasAsync() =>
            await context.Categorias
                .Include(c => c.Candidatos)
                .Select(c => new CategoriaReturnDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Description = c.Description,
                    Candidatos = c.Candidatos.Select(ca => new CandidatoWithCategoriaDTO
                    {
                        Id = ca.Id,
                        Nome = ca.Nome,
                        Description = ca.Description,
                        PhotoUrl = ca.PhotoUrl,
                    }).ToList()
                }).ToListAsync();

        public async Task<CategoriaReturnDTO?> GetCategoriaByIdAsync(Guid id)=>
            await context.Categorias
                .Where(c => c.Id == id)
                .Select(c => new CategoriaReturnDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Description = c.Description,
                    Candidatos = c.Candidatos.Select(ca => new CandidatoWithCategoriaDTO
                    {
                        Id = ca.Id,
                        Nome = ca.Nome,
                        Description = ca.Description,
                        PhotoUrl = ca.PhotoUrl,
                    }).ToList()
                }).FirstOrDefaultAsync();

        public async Task<CategoriaDTO?> UpdateCategoriaAsync(Guid id, CategoriaCreateDTO model)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (categoria == null)
                return null;
            categoria.Nome = model.Nome;
            categoria.Description = model.Description;
            context.Categorias.Update(categoria);

            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Description = categoria.Description,
            };
        }
    }
}
