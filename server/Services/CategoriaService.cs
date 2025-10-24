using server.DTOs;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.UnitOfWork;

namespace server.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repositoy;
        private readonly SaveChages saveChages;

        public CategoriaService(ICategoriaRepository repositoy, SaveChages saveChages)
        {
            _repositoy = repositoy;
            this.saveChages = saveChages;
        }

        public async Task<CategoriaDTO?> AddCategoriaAsnyc(CategoriaCreateDTO model)
        {
            var res = await this.CategoriaExistsAsync(model.Nome);

            if (res)
            {
                return null;
            }
            var categoria = await _repositoy.AddCategoriaAsnyc(model);
            await saveChages.SaveChangesAsync();

            return categoria;
        }

        public async Task<bool> CategoriaExistsAsync(string nome) => await _repositoy.CategoriaExistsAsync(nome);

        public async Task<bool> DeleteCategoriaAsync(Guid id)
        {

            await _repositoy.DeleteCategoriaAsync(id);
            await saveChages.SaveChangesAsync();

            return true;
        }

        public async Task<List<CategoriaReturnDTO>> GetAllCategoriasAsync()=>
            await _repositoy.GetAllCategoriasAsync();

        public async Task<CategoriaReturnDTO?> GetCategoriaByIdAsync(Guid id) =>
            await _repositoy.GetCategoriaByIdAsync(id);

        public async Task<CategoriaDTO?> UpdateCategoriaAsync(Guid id, CategoriaCreateDTO model)
        {
            var categoria = await _repositoy.UpdateCategoriaAsync(id, model);
            await saveChages.SaveChangesAsync();
            return categoria;
        }
    }
}
