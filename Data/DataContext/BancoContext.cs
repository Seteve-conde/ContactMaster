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
    }
}
