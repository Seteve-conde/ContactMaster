using Dados.Repositorio;
using DataContext;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace Ioc
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<BancoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            services.AddScoped<IBonusRepositorio, BonusRepositorio>();
        }
    }
}
