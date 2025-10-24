using Microsoft.EntityFrameworkCore;
using server.ApplicationDbContext;
using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories
{
    public class VotoRepository(AppDbContext context) : IVotoRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<VotoReturnDTO?> CreateVotoAsync(VotoCreateDTO model)
        {
            try
            {
                // Verifica se o eleitor existe
                var eleitorExists = await _context.Eleitores.AnyAsync(e => e.Id == model.EleitorId);
                if (!eleitorExists)
                {
                    return null;
                }

                // Verifica se o candidato existe e pertence à categoria informada
                var candidatoNaCategoria = await _context.Candidatos
                    .AnyAsync(c => c.Id == model.CandidatoId && c.CategoriaId == model.CategoriaId);

                if (!candidatoNaCategoria)
                {
                    // candidato não existe ou não pertence à categoria
                    return null;
                }

                // Verifica se o eleitor já votou nessa categoria
                var exist = await _context.Votos
                    .AnyAsync(v => v.EleitorId == model.EleitorId && v.CategoriaId == model.CategoriaId);

                if (exist)
                {
                    return null;
                }

                var voto = new Voto
                {
                    EleitorId = model.EleitorId,
                    CandidatoId = model.CandidatoId,
                    CategoriaId = model.CategoriaId
                };

                await _context.Votos.AddAsync(voto);
                await _context.SaveChangesAsync();

                return await _context.Votos
                    .Where(v => v.Id == voto.Id)
                    .Select(v => new VotoReturnDTO
                    {
                        Id = v.Id,
                        Eleitor = v.Eleitor.Nome,
                        Candidato = v.Candidato.Nome,
                        Categoria = v.Categoria.Nome
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<VotoReturnDTO>> GetAllVotosAsync()
        {
            return await _context.Votos
                    .Select(v => new VotoReturnDTO
                    {
                        Id = v.Id,
                        Eleitor = v.Eleitor.Nome,
                        Candidato = v.Candidato.Nome,
                        Categoria = v.Categoria.Nome
                    }).ToListAsync();
        }

        public async Task<List<VotoReturnDTO>> GetVotosByCategoriaAsync(Guid id)
        {
            return await _context.Votos
                .Where(v => v.CategoriaId == id)
                .Select(v => new VotoReturnDTO
                {
                    Id = v.Id,
                    Eleitor = v.Eleitor.Nome,
                    Candidato = v.Candidato.Nome,
                    Categoria = v.Categoria.Nome
                }).ToListAsync();

        }

        public async Task<int> TotalVotosCategoriAsync(Guid id)
        {
            return  await _context.Votos.CountAsync(v => v.CategoriaId == id);
        }

        public async Task<int> TotalVotosGeralAsync()=>
            await _context.Votos.CountAsync();
    }
}
