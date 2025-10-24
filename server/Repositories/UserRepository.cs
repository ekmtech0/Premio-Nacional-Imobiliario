using Microsoft.EntityFrameworkCore;
using server.ApplicationDbContext;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories
{
    public class UserRepository<TUser> : IUserRepository<TUser> where TUser : User
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TUser> _dbSet;
        public UserRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TUser>();
        }

        public async Task<TUser> AddAsync(TUser user)
        {
            await _dbSet.AddAsync(user);
            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Id == id) ??throw new KeyNotFoundException("User not found");
            _dbSet.Remove(user);
        }

        public async Task<List<TUser>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<TUser?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Id == id);
        }

        public void UpdateAsync(TUser user)
        {
            _dbSet.Update(user);
        }
    }
}
