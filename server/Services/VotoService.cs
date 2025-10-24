using server.DTOs;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository repository;

        public VotoService(IVotoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<VotoReturnDTO?> CreateVotoAsync(VotoCreateDTO model)
        {
            var voto = await repository.CreateVotoAsync(model);
            return voto;
        }

        public async Task<List<VotoReturnDTO>> GetAllVotosAsync() =>
            await repository.GetAllVotosAsync();

        public async Task<List<VotoReturnDTO>> GetVotosByCategoriaAsync(Guid id)=>
            await repository.GetVotosByCategoriaAsync(id);

        public async Task<int> TotalVotosCategoriAsync(Guid id)=>
            await repository.TotalVotosCategoriAsync(id);

        public async Task<int> TotalVotosGeralAsync()=>
            await repository.TotalVotosGeralAsync();
    }
}
