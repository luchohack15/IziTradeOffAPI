using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace IziTradeOff.API.Configuration
{
    public class ToolsConfig
    {
        /// <summary>
        /// Metodo estatico que aplica las configuraciones de herramientas adicionales
        /// </summary>
        /// <param name="Configuration">Interfaz de configuracion</param>
        /// <param name="services">Interfaz que registra los servicios</param>
        /// Johnny Arcia
        public static void Config(IConfiguration Configuration, IServiceCollection services)
        {
            //// Inyectar TwilioService
            //services.AddScoped<ITwilioService, TwilioService>();
            //// Injectar Servicio de Email
            //var emailConfig = Configuration
            //    .GetSection("EmailConfiguration")
            //    .Get<EmailConfiguration>();
            //services.AddSingleton(emailConfig);
            //services.AddScoped<IEmailSender, EmailSender>();
            // IUrlHelper
            services.AddHttpContextAccessor();
        }
    }
}
