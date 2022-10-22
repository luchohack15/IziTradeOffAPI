using IziTradeOff.Application.Interfaces;
using IziTradeOff.Application.Security;
using IziTradeOff.Persistence.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IziTradeOff.API.Configuration
{
    public static class GenericInterfaceConfig
    {
        /// <summary>
        /// Metodo estatico que aplica las configuraciones del contexto
        /// </summary>
        /// <param name="Configuration">Interfaz de configuracion</param>
        /// <param name="services">Interfaz que registra los servicios</param>
        /// Johnny Arcia
        public static void Config(IConfiguration Configuration, IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
