using System;
using Microsoft.EntityFrameworkCore;
using server.ApplicationDbContext;
using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories;

public class EleitorRepository : UserRepository<Eleitor>, IEleitorRepository
{
    public EleitorRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Eleitor?> GetByEmailAsync(string email)
    {
        return await _context.Eleitores.FirstOrDefaultAsync(e => e.Email == email);
    }

    public Task<EleitorWithVotosDTO?> GetEleitorByIdAsync(Guid Id)
    {
        return _context.Eleitores
            .Include(e => e.Votos)
            .Select(e => new EleitorWithVotosDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                Email = e.Email,
                Votos = e.Votos.Select(v => new VotoDTO
                {
                    Id = v.Id,
                    Candidato = v.Candidato.Nome,
                    //DataVoto = v.DataVoto
                    Categoria = v.Categoria.Nome
                }).ToList()
            })
            .FirstOrDefaultAsync(e => e.Id == Id);
    }

    public async Task<List<EleitorWithVotosDTO>> GetEleitorsAsync()
    {
        return await _context.Eleitores
            .Include(e => e.Votos)
            .Select(e => new EleitorWithVotosDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                Email = e.Email,
                Votos = e.Votos.Select(v => new VotoDTO
                {
                    Id = v.Id,
                    Candidato = v.Candidato.Nome,
                    //DataVoto = v.DataVoto
                    Categoria = v.Categoria.Nome
                }).ToList()
            }).ToListAsync();
    }

    public async Task<bool> IsCreatedAsync(string email)
    {
        return await _context.Eleitores.AnyAsync(e => e.Email == email);
    }
}
