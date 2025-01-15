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
                    builder.AllowAnyOrigin()    
                           .AllowAnyMethod()   
                           .AllowAnyHeader();   
                });
            });

            services.AddDbContext<BancoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();

            services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            services.AddScoped<IContatoService, ContatoService>();            

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISessaoService, SessaoService>();

            services.AddDistributedMemoryCache(); 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  
                options.Cookie.HttpOnly = true;                  
                options.Cookie.IsEssential = true;               
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
            
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseSession();

            app.UseCors("PermitirTudo");

            app.UseRouting();

            
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  
            });
        }

    }
}
