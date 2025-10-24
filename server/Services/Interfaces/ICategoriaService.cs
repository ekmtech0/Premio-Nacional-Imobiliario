using server.DTOs;

namespace server.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<bool> CategoriaExistsAsync(string nome);
        Task<CategoriaDTO?> AddCategoriaAsnyc(CategoriaCreateDTO model);

        Task<List<CategoriaReturnDTO>> GetAllCategoriasAsync();

        Task<CategoriaReturnDTO?> GetCategoriaByIdAsync(Guid id);

        Task<bool> DeleteCategoriaAsync(Guid id);

        Task<CategoriaDTO?> UpdateCategoriaAsync(Guid id, CategoriaCreateDTO model);
    }
}
