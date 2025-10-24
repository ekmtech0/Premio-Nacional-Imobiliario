using server.ApplicationDbContext;

namespace server.UnitOfWork
{
    public class SaveChages
    {
        private readonly AppDbContext _context;

        public SaveChages(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
