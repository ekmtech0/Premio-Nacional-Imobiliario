using Microsoft.EntityFrameworkCore;
using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories
{
    public class CandidatoRepository : UserRepository<Candidato>, ICandidatoRepository
    {
        //private readonly ApplicationDbContext.AppDbContext _context;
        public CandidatoRepository(ApplicationDbContext.AppDbContext context) : base(context)
        {
        }

        public async Task<CandidatoDTO?> AddCadidatoAsync(CandidatoDTO model)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == model.CategoriaId);
            
            if (categoria == null)
                return null;

            var candidato = new Candidato
            {
                Categoria = categoria,
                Nome = model.Nome,
                Description = model.Description,
                PhotoUrl = model.PhotoUrl,
                Role = "user",
            };

            var user = await _context.Candidatos.AddAsync(candidato);

            return new CandidatoDTO
            {
                CategoriaId = user.Entity.Categoria.Id,
                Nome = user.Entity.Nome,
                Description = user.Entity.Description,
                PhotoUrl = user.Entity.PhotoUrl
            };
        }
    }
}
