using System;
using System.Collections.Generic;
using System.Text;

namespace IziTradeOff.Domain
{
    public class ClaseBase
    {
        /// <summary>
        /// Id del modelo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Estado Activo-Inactivo
        /// </summary>
        public bool Estado { get; set; }
    }
}
