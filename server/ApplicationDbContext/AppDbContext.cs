using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Eleitor> Eleitores => Set<Eleitor>();
        public DbSet<Voto> Votos => Set<Voto>();
        public DbSet<Candidato> Candidatos => Set<Candidato>();
        public DbSet<Categoria> Categorias => Set<Categoria>();

        public DbSet<Adm> Adms => Set<Adm>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais podem ser feitas aqui, se necessário

            modelBuilder.Entity<Voto>()
                .HasIndex(v => new { v.EleitorId, v.CategoriaId })
                .IsUnique();
        }
    }
}
