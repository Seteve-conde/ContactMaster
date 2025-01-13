using ContactMaster.Services;
using ContactMasterService.Interfaces;
using ContactMasterService.Services;
using Dados.Repositorio;
using DataContext;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ioc
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<BancoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            services.AddScoped<IBonusRepositorio, BonusRepositorio>();
            services.AddScoped<IContatoService, ContatoService>();
            services.AddScoped<IBonusService, BonusService>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepositorio, LoginRepositorio>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IContatoApiService, ContatoApiService>();

        }
    }
}
