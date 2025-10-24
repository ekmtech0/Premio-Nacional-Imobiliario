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

        public async Task<CandidatoDTO> CadastrarCandidatoAsync(CandidatoDTO model)
        {
            var user = await repository.AddCadidatoAsync(model);

            if(user is null)
                throw new Exception("Erro ao cadastrar candidato");

            await _saveChages.SaveChangesAsync();

            return new CandidatoDTO
            {
                Nome = user.Nome,
                Description = user.Description,
                PhotoUrl = user.PhotoUrl,
                CategoriaId = user.CategoriaId
            };
        }

        public async Task<bool> DeleteCandidatoAsycnc(Guid id)
        {
            await repository.DeleteAsync(id);
            await _saveChages.SaveChangesAsync();
            return true;
        }

        public async Task<List<CandidatoDTO>> GetAllCandidatosAsync()
        {
            var candidatos = await repository.GetAllAsync();

            return candidatos.Select(c => new CandidatoDTO
            {
                Nome = c.Nome,
                Description = c.Description,
                PhotoUrl = c.PhotoUrl,
                CategoriaId = c.CategoriaId
            }).ToList();
        }

        public async Task<CandidatoDTO?> GetCandidatoByIdAsync(Guid id)
        {
            var candidato = await repository.GetByIdAsync(id);

            return candidato is null ? null : new CandidatoDTO
            {
                Nome = candidato.Nome,
                Description = candidato.Description,
                PhotoUrl = candidato.PhotoUrl,
                CategoriaId = candidato.CategoriaId
            };
        }

        public async Task<CandidatoDTO?> UpdateCandidatoAsync(Guid id, CandidatoDTO model)
        {
            // Verifica se o candidato existe
            var candidato = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Usuário não existe");
            candidato.Nome = model.Nome;
            candidato.Description = model.Description;
            candidato.PhotoUrl = model.PhotoUrl;
            candidato.CategoriaId = model.CategoriaId;

            repository.UpdateAsync(candidato);

            await _saveChages.SaveChangesAsync();
            return new CandidatoDTO
            {
                Nome = candidato.Nome,
                Description = candidato.Description,
                PhotoUrl = candidato.PhotoUrl,
                CategoriaId = candidato.CategoriaId
            };
        }
    }
}
