using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.UnitOfWork;

namespace server.Services
{
    public class CandidatoService(ICandidatoRepository repository, SaveChages saveChages) : ICandidatoService
    {
        private readonly ICandidatoRepository repository = repository;
        private readonly SaveChages _saveChages = saveChages;

        public async Task<CandidatoReturnDTO> CadastrarCandidatoAsync(CandidatoDTO model)
        {
            var user = await repository.AddCadidatoAsync(model);

            if(user is null)
                throw new Exception("Erro ao cadastrar candidato");

            await _saveChages.SaveChangesAsync();

            return new CandidatoReturnDTO
            {
                Nome = user.Nome,
                Description = user.Description,
                PhotoUrl = user.PhotoUrl,
                Categoria = user.Categoria
            };
        }

        public async Task<bool> DeleteCandidatoAsycnc(Guid id)
        {
            await repository.DeleteAsync(id);
            await _saveChages.SaveChangesAsync();
            return true;
        }

        public async Task<List<CandidatoReturnDTO>> GetAllCandidatosAsync()
        {
            var candidatos = await repository.GetAllCandiditatosAsync();

            return candidatos;
        }

        public async Task<CandidatoReturnDTO?> GetCandidatoByIdAsync(Guid id)
        {
            var candidato = await repository.GetCandidatoById(id);
            return candidato;
        }

        public async Task<CandidatoReturnDTO?> UpdateCandidatoAsync(Guid id, CandidatoDTO model)
        {
            // Verifica se o candidato existe
            var candidato = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Usuário não existe");
            candidato.Nome = model.Nome;
            candidato.Description = model.Description;
            candidato.PhotoUrl = model.PhotoUrl;
            candidato.CategoriaId = model.CategoriaId;

            repository.UpdateAsync(candidato);

            await _saveChages.SaveChangesAsync();
            return new CandidatoReturnDTO
            {
                Nome = candidato.Nome,
                Description = candidato.Description,
                PhotoUrl = candidato.PhotoUrl,
                Categoria = candidato.Categoria.Nome
            };
        }
        public async Task<List<ListaDosMaisVotadosDTO>> GetListaDosMaisVotadosDTOAsync()
        {
            return await repository.GetListaDosMaisVotadosDTOAsync(); 
        }
    }
}
