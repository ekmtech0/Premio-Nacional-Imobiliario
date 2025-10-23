using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IUserRepository<TUser> where TUser : User
    {
        Task<TUser?> GetByIdAsync(Guid id);
        Task<List<TUser>> GetAllAsync();
        Task<TUser> AddAsync(TUser user);
        void UpdateAsync(TUser user);
        Task DeleteAsync(Guid id);
    }
}
