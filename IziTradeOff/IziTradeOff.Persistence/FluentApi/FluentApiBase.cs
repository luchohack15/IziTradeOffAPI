using IziTradeOff.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IziTradeOff.Persistence.FluentApi
{
    public class FluentApiBase
    {
        /// <summary>
        /// funcion que crea las configuraciones en el model builder
        /// </summary>
        /// <param name="modelBuilder">modelo de configuracion</param>
        /// Johnny Arcia
        public void Map(ref ModelBuilder modelBuilder)
        {
            new TraduccionConfiguration().Configure(modelBuilder.Entity<Traduccion>());
        }
    }
}