using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IziTradeOff.API.Configuration
{
    public class CorsConfig
    {
        /// <summary>
        /// Metodo estatico que aplica la configuracion de cors
        /// </summary>
        /// <param name="Configuration">Interfaz de configuracion</param>
        /// <param name="services">Interfaz que registra los servicios</param>
        /// Johnny Arcia
        public static void Config(IConfiguration Configuration, IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });
        }
    }
}
