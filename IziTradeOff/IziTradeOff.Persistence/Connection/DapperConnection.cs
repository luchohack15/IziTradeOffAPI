using IziTradeOff.Application.Interfaces;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IziTradeOff.Persistence.Connection
{
    public class DapperConnection : IDapperConnection, IDisposable
    {
        private IDbConnection connection; //Conexion global Dapper
        /// <summary>
        /// Constructor de la clase con las configuraciones
        /// </summary>
        /// Johnny Arcia
        public DapperConnection()
        {
        }
        /// <summary>
        /// Metodo que cierra conexion con Dapper
        /// </summary>
        /// Johnny Arcia
        public void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Metodo que genera una conexion singleton de Dapper
        /// </summary>
        /// <returns>Conexions con la bd Dapper</returns>
        /// Johnny Arcia
        public IDbConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(SingletonConexiones.ConnectionString);
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// Se implementa el cierre de conexion automatico,pero las consultas deben estar dentro de un using
        /// </summary>
        /// <remarks>Johnny Arcia</remarks>
        public void Dispose()
        {
            CloseConnection();
        }
    }
}
