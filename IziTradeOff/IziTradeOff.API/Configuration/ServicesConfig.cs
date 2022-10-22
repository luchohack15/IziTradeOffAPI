using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IziTradeOff.Application.Interfaces.Services;
using IziTradeOff.Service.Services;

namespace IziTradeOff.API.Configuration
{
    public static class ServicesConfig
    {
        public static void Config(IConfiguration Configuration, IServiceCollection services)
        {
            services.AddScoped<ITraduccionService, TraduccionService>();
        }
    }
}
