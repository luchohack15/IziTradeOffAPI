using IziTradeOff.Persistence.Connection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IziTradeOff.API
{
    public class Program
    {
        //public static async Task Main(string[] args)
        //{
        //    var webHost = CreateHostBuilder(args).Build();

        //    //Migracion de base datos cuando se ejecuta la api
        //    //await ApplyMigrations(webHost.Services);

        //    await webHost.RunAsync();
        //}

        ///// <summary>
        ///// Aplica las migraciones pendientes cuando se ejecuta la api
        ///// </summary>
        ///// <param name="serviceProvider">Host proveedor de servicios</param>
        ///// <returns>Tarea asincrona</returns>
        ///// Johnny Arcia
        //private static async Task ApplyMigrations(IServiceProvider serviceProvider)
        //{
        //    using var scope = serviceProvider.CreateScope();

        //    await using IConexion dbContext = scope.ServiceProvider.GetRequiredService<IConexion>();

        //    await dbContext.Database.MigrateAsync();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static IWebHostBuilder CreateDefaultBuilder(string[] args)
        {
            var builder = new WebHostBuilder();

            if (string.IsNullOrEmpty(builder.GetSetting(WebHostDefaults.ContentRootKey)))
            {
                builder.UseContentRoot(Directory.GetCurrentDirectory());
            }
            if (args != null)
            {
                builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());
            }

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                if (env.IsDevelopment())
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true);
                    }
                }

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            });
            builder.UseStartup<Startup>();
            return builder;
        }
    }
}
