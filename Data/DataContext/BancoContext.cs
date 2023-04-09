using Microsoft.EntityFrameworkCore;
using Dominio.Models;

namespace DataContext
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<BonusModel> bonusModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<BonusModel>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
