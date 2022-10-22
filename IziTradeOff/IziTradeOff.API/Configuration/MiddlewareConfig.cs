using IziTradeOff.Application.Middleware;
using Microsoft.AspNetCore.Builder;

namespace IziTradeOff.API.Configuration
{
    public static class MiddlewareConfig
    {
        /// <summary>
        /// Metodo configuraciones del middeware en el startup
        /// </summary>
        /// <param name="app">interfaz con el builder de la aplicacion</param>
        /// Johnny Arcia
        public static void Config(IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorMiddleware>();
        }
    }
}
