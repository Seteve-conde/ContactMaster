using ContactMaster.Services;
using ContactMasterService.Services;
using Dados.Repositorio;
using DataContext;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("PermitirTudo", builder =>
                {
                    builder.AllowAnyOrigin()    // Permite qualquer origem
                           .AllowAnyMethod()    // Permite qualquer método HTTP (GET, POST, PUT, DELETE, etc.)
                           .AllowAnyHeader();   // Permite qualquer cabeçalho
                });
            });

            services.AddDbContext<BancoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();

            services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            services.AddScoped<IContatoService, ContatoService>();
            services.AddScoped<ISessaoService, SessaoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDistributedMemoryCache(); 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  // Tempo máximo de inatividade da sessão
                options.Cookie.HttpOnly = true;                  // Garante que o cookie de sessão seja acessado apenas via HTTP
                options.Cookie.IsEssential = true;               // Garante que o cookie seja essencial para a aplicação
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MasterApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasterApi v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Habilita o uso de redirecionamento HTTPS
            app.UseHttpsRedirection();

            // Habilita os arquivos estáticos (se necessário)
            app.UseStaticFiles();

            // Habilita o uso de sessões
            app.UseSession();

            app.UseCors("PermitirTudo");

            // Configura o roteamento
            app.UseRouting();

            // Adiciona autorização (se necessário)
            app.UseAuthorization();

            // Define os endpoints da API
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  // Mapeia as rotas dos controllers
            });
        }

    }
}
