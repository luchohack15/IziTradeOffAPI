using System.Data;

namespace IziTradeOff.Application.Interfaces
{
    public interface IDapperConnection
    {
        /// <summary>
        /// Implementacion cierre de conexion
        /// </summary>
        void CloseConnection();
        /// <summary>
        /// Implementacion obtener conexion
        /// </summary>
        /// <returns>Conexion Dapper</returns>
        IDbConnection GetConnection();
    }
}
