using server.DTOs;
using server.Models;

namespace server.Repositories.Interfaces
{
    public interface ICandidatoRepository : IUserRepository<Candidato>
    {
        //Adicione métodos específicos do Candidato aqui, se necessário

        public Task<CandidatoDTO?> AddCadidatoAsync(CandidatoDTO model);
    }
}
