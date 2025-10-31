using server.DTOs;
using server.Models;

namespace server.Repositories.Interfaces
{
    public interface ICandidatoRepository : IUserRepository<Candidato>
    {
        //Adicione métodos específicos do Candidato aqui, se necessário

        public Task<CandidatoReturnDTO?> AddCadidatoAsync(CandidatoDTO model);
        public Task<List<CandidatoReturnDTO>> GetAllCandiditatosAsync();
        public Task<CandidatoReturnDTO?> GetCandidatoById(Guid id);
        public Task<List<ListaDosMaisVotadosDTO>> GetListaDosMaisVotadosDTOAsync();
    }
}
