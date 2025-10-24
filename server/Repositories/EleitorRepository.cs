using System;
using Microsoft.EntityFrameworkCore;
using server.ApplicationDbContext;
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

    public async Task<bool> IsCreatedAsync(string email)
    {
        return await _context.Eleitores.AnyAsync(e => e.Email == email);
    }
}
