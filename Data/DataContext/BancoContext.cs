using Microsoft.EntityFrameworkCore;
using Dominio.Models;
using Dados.Map;

namespace DataContext
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<BonusModel> BonusModels { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());

            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<BonusModel>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
