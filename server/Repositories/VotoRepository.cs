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

        public async Task<VotoResult> CreateVotoAsync(VotoCreateDTO model)
        { // Verifica se o navegador já votou nesta 

            //Verifica se o candidato está na categória
            var exists = await _context.Candidatos.AnyAsync(c =>
                c.Id == model.CandidatoId &&
                c.CategoriaId == model.CategoriaId);
            if (!exists)
                return new VotoResult
                {
                    Sucesso = false,
                    Mensagem = "Candidato não pertence à categoria especificada."
                };

            var jaVotou = await _context.Votos.AnyAsync(v =>
                v.BrowserId == model.BrowserId &&
                v.CategoriaId == model.CategoriaId);

            if (jaVotou)
            {
                return new VotoResult
                {
                    Sucesso = false,
                    Mensagem = "Este navegador já votou nesta categoria."
                };
            }

            await _context.Votos.AddAsync(new Voto
            {
                BrowserId = model.BrowserId,
                CandidatoId = model.CandidatoId,
                CategoriaId = model.CategoriaId,
                DataVoto = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            var voto = await _context.Votos
                .Where(v => v.BrowserId == model.BrowserId &&
                            v.CandidatoId == model.CandidatoId &&
                            v.CategoriaId == model.CategoriaId)
                .Select(v => new VotoReturnDTO
                {
                    Id = v.Id,
                    BrowserId = v.BrowserId,
                    CandidatoId = v.CandidatoId,
                    CategoriaId = v.CategoriaId,
                    DataVoto = v.DataVoto
                })
                .FirstOrDefaultAsync();
            return new VotoResult
            {
                Sucesso = true,
                Mensagem = "Voto registrado com sucesso.",
                Dados = voto
            };
        }

        public async Task<List<VotoReturnDTO>> GetAllVotosAsync()
        {
            return await _context.Votos
                .Select(v => new VotoReturnDTO
                {
                    Id = v.Id,
                    BrowserId = v.BrowserId,
                    CandidatoId = v.CandidatoId,
                    CategoriaId = v.CategoriaId,
                    DataVoto = v.DataVoto
                })
                .ToListAsync();
        }

        public async Task<List<VotoReturnDTO>> GetVotosByCategoriaAsync(Guid id)
        {
            return await _context.Votos
                .Where(v => v.CategoriaId == id)
                .Select(v => new VotoReturnDTO
                {
                    Id = v.Id,
                    BrowserId = v.BrowserId,
                    CandidatoId = v.CandidatoId,
                    CategoriaId = v.CategoriaId,
                    DataVoto = v.DataVoto
                })
                .ToListAsync();
        }

        public async Task<int> TotalVotosCategoriAsync(Guid id)
        {
            return  await _context.Votos.CountAsync(v => v.CategoriaId == id);
        }

        public async Task<int> TotalVotosGeralAsync()=>
            await _context.Votos.CountAsync();
    }
}
