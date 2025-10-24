using server.DTOs;

namespace server.Services.Interfaces
{
    public interface IVotoService
    {
        Task<VotoReturnDTO?> CreateVotoAsync(VotoCreateDTO model);
        Task<List<VotoReturnDTO>> GetAllVotosAsync();
        Task<List<VotoReturnDTO>> GetVotosByCategoriaAsync(Guid id);
        Task<int> TotalVotosCategoriAsync(Guid id);
        Task<int> TotalVotosGeralAsync();
    }
}
