using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IziTradeOff.Application.Interfaces.Services;
using IziTradeOff.Service.Services;
using IziTradeOff.Application.Interfaces.Services.Query;
using IziTradeOff.Service.Query;
using IziTradeOff.Application.Interfaces.Services.Command;
using IziTradeOff.Service.Command;

namespace IziTradeOff.API.Configuration
{
    public static class ServicesConfig
    {
        public static void Config(IConfiguration Configuration, IServiceCollection services)
        {
            //Services
            services.AddScoped<ITraduccionService, TraduccionService>();
           
            //Querys
            services.AddScoped<ITraductorQuery, TraduccionQuery>();
            services.AddScoped<ILoginQuery, LoginQuery>();
            //Commands
            services.AddScoped<ITraductorCommand, TraduccionCommand>();
            services.AddScoped<ILoginCommand, LoginCommand>();
        }
    }
}
