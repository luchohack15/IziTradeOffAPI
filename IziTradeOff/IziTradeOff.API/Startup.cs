using FluentValidation;
using IziTradeOff.API.Configuration;
using IziTradeOff.Application.Dtos;
using IziTradeOff.Application.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IziTradeOff.API
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
            services.AddMvc();
            services.AddControllers();

            ConnectionConfig.Config(Configuration, services);
            services.AddOptions();
            //Se agrega referencia del middleware para captura de excepciones
            services.AddTransient<ErrorMiddleware>();

            //Configuracion del dapper
            DapperConfig.Config(Configuration, services);

            //Configura la seguridad del identity
            SeguridadConfig.Config(Configuration, services);

            //Configura las herramientas adicionales
            ToolsConfig.Config(Configuration, services);

            //Configuracion del autoMapper
            services.AddAutoMapper(typeof(MappingProfiles));

            //Configuracion del swagger
            SwaggerConfig.Config(Configuration, services);

            //Configuracion de los cors
            CorsConfig.Config(Configuration, services);

            //Configuracion de los servicios
            ServicesConfig.Config(Configuration, services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Configuracion del middleware
            MiddlewareConfig.Config(app);

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/errors", "?code={0}");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseCors("CorsRule");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaranathaCargoExpres-Api v1"));
        }
    }
}
