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

        public async Task<CandidatoReturnDTO?> AddCadidatoAsync(CandidatoDTO model)
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
            return new CandidatoReturnDTO
           {
                Id = user.Entity.Id,
                Nome = user.Entity.Nome,
                Description = user.Entity.Description,
                PhotoUrl = user.Entity.PhotoUrl,
                Categoria = user.Entity.Categoria.Nome,
                CategoriaId = user.Entity.Categoria.Id
           };
        }

        public async Task<List<CandidatoReturnDTO>> GetAllCandiditatosAsync()
        {
            return await _context.Candidatos
                .Include(c => c.Categoria)
                .Select(c => new CandidatoReturnDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Description = c.Description,
                    PhotoUrl = c.PhotoUrl,
                    Categoria = c.Categoria.Nome,
                    CategoriaId = c.Categoria.Id,
                    Votos = c.Votos.Count
                })
                .ToListAsync();
        }

        public async Task<CandidatoReturnDTO?> GetCandidatoById(Guid id)
        {
            return await _context.Candidatos
                .Include(c => c.Categoria)
                .Include(c => c.Votos)
                .Select(c => new CandidatoReturnDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Categoria = c.Categoria.Nome,
                    Description = c.Description,
                    PhotoUrl = c.PhotoUrl,
                    CategoriaId = c.Categoria.Id,
                    Votos = c.Votos.Count
                })
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<ListaDosMaisVotadosDTO>> GetListaDosMaisVotadosDTOAsync()
        {
            return await _context.Candidatos
                .Include(v => v.Votos)
                .Select(c => new ListaDosMaisVotadosDTO
                {
                    Nome = c.Nome,
                    TotalVotos = c.Votos.Count
                })
                .OrderByDescending(c => c.TotalVotos)
                .ToListAsync();
        }
    }
}
