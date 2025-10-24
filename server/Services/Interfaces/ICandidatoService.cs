using server.DTOs;

namespace server.Services.Interfaces
{
    public interface ICandidatoService
    {
        Task<CandidatoDTO> CadastrarCandidatoAsync(CandidatoDTO model);
        Task<List<CandidatoDTO>> GetAllCandidatosAsync();

        Task<bool> DeleteCandidatoAsycnc(Guid id);

        Task<CandidatoDTO?> GetCandidatoByIdAsync(Guid id);

        Task<CandidatoDTO?> UpdateCandidatoAsync(Guid id, CandidatoDTO model);
    }
}
