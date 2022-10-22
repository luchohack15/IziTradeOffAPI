using Microsoft.EntityFrameworkCore;

namespace IziTradeOff.Persistence.Connection
{
    public static class SingletonConexiones
    {
        /// <summary>
        /// Conexion principal de la base de datos MCE
        /// </summary>
        public static DbContextOptionsBuilder optionsConexion;
        /// <summary>
        /// Conexion con la base de datos MCE
        /// </summary>
        public static string ConnectionString;
    }
}
