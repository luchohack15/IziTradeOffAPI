using IziTradeOff.Persistence.Connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Internal;

namespace IziTradeOff.API.Configuration
{
    public static class ConnectionConfig
    {
        public static void Config(IConfiguration Configuration, IServiceCollection services)
        {
            //Se busca el tipo de base de datos configurada
            var db = Configuration.GetValue<string>("Database");
            //Se obtiene la ruta del contexto por medio de su NameSpace --MCargoExpress.Persistence.Mysql
            string assemblyName = typeof(ConexionMysql).Namespace;
            string mySqlConnectionStr = Configuration.GetConnectionString("mysqlConnection");
            var serverVersion = ServerVersion.AutoDetect(mySqlConnectionStr);

            //Se inicializa las conexiones globales
            SingletonConexiones.optionsConexion = new DbContextOptionsBuilder<IConexion>().UseMySql(mySqlConnectionStr, mysqlOptions =>
            {
                mysqlOptions.ServerVersion(serverVersion);
            });
            SingletonConexiones.ConnectionString = mySqlConnectionStr;
            services.AddDbContextPool<IConexion>(options => options.UseMySql(mySqlConnectionStr,
                //Se configura la ruta de migraciones por defecto en este contexto
                optionsBuilder => {
                    optionsBuilder.MigrationsAssembly(assemblyName);
                    optionsBuilder.ServerVersion(serverVersion);
                    }));

        }
    }
}
