using System;
using server.DTOs;
using server.Models;

namespace server.Repositories.Interfaces;

public interface IEleitorRepository : IUserRepository<Eleitor>
{
    Task<Eleitor?> GetByEmailAsync(string email);
    Task<bool> IsCreatedAsync(string email);
    Task<EleitorWithVotosDTO?> GetEleitorByIdAsync(Guid Id);
    Task<List<EleitorWithVotosDTO>> GetEleitorsAsync();
}
