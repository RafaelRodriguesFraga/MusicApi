using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MusicApi.Core;
using MusicApi.Core.Services;
using MusicApi.Data;
using MusicApi.Services;
using System;
using System.IO;
using System.Reflection;

namespace MusicApi.Api
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
            services.AddControllers();

            services.AddScoped<IUnityOfWork, UnityOfWork>();

            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IArtistService, ArtistService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<MusicDbContext>(options => 
                    options.UseNpgsql(Configuration.GetConnectionString("Default"), 
                    x => x.MigrationsAssembly("MusicApi.Data")));

            //Registrar o gerador do swagger definindo um ou mais document os Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Catalogo", Version = "v1" });

                // Caminho para o Swagger JSON e UI
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Habilita o Middleware para servir o SwaggerUI especificando o endpoint
            app.UseSwagger();

            //Habilita o Middleware para servir o SwaggerUI especificando o endpoint Swaggger JSON
            app.UseSwaggerUI(c =>
            {
               
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Catálogo V1");
            });
        }
    }
}
