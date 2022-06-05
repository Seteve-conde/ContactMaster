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
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BancoContext>(opt => opt.UseSqlServer(configuration["ConnectionString"]));
            services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
        }
    }
}
