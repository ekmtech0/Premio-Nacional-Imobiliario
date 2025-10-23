using server.DTOs;

namespace server.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<bool> CategoriaExistsAsync(string nome);
        Task<CategoriaReturnDTO> AddCategoriaAsnyc(CategoriaDTO model);

        Task<List<CategoriaReturnDTO>> GetAllCategoriasAsync();

        Task<CategoriaReturnDTO?> GetCategoriaByIdAsync(Guid id);

        Task<bool> DeleteCategoriaAsync(Guid id);

        Task<CategoriaReturnDTO?> UpdateCategoriaAsync(Guid id, CategoriaDTO model);
    }
}
