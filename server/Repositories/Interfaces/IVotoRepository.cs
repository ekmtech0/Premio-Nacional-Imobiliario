using server.DTOs;

namespace server.Repositories.Interfaces
{
    public interface IVotoRepository
    {
        Task<VotoResult> CreateVotoAsync(VotoCreateDTO model);
        Task<List<VotoReturnDTO>> GetAllVotosAsync();
        Task<List<VotoReturnDTO>> GetVotosByCategoriaAsync(Guid id);
        Task<int> TotalVotosCategoriAsync(Guid id);

        Task<int> TotalVotosGeralAsync();
    }
}
