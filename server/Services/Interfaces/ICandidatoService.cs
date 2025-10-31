using server.DTOs;

namespace server.Services.Interfaces
{
    public interface ICandidatoService
    {
        Task<CandidatoReturnDTO> CadastrarCandidatoAsync(CandidatoDTO model);
        Task<List<CandidatoReturnDTO>> GetAllCandidatosAsync();

        Task<bool> DeleteCandidatoAsycnc(Guid id);

        Task<CandidatoReturnDTO?> GetCandidatoByIdAsync(Guid id);

        Task<CandidatoReturnDTO?> UpdateCandidatoAsync(Guid id, CandidatoDTO model);
        Task<List<ListaDosMaisVotadosDTO>> GetListaDosMaisVotadosDTOAsync();
    }
}
