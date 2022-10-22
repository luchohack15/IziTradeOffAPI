namespace IziTradeOff.Application.Interfaces
{
    public class DapperConnectionConfig
    {
        /// <summary>
        /// Conexion MySql
        /// </summary>
        public string mysqlConnection { get; set; }
        /// <summary>
        /// Conexion SqlServer
        /// </summary>
        public string sqlServerConnection { get; set; }
    }
}
