using System;
using System.Collections.Generic;
using System.Text;

namespace IziTradeOff.Domain
{
    public class Traduccion : ClaseBase
    {
        /// <summary>
        /// codigo clave que identifica la traduccion
        /// </summary>
        public string Llave { get; set; }
        /// <summary>
        /// Valor de la clave
        /// </summary>
        public string Valor { get; set; }
        /// <summary>
        /// Lenguaje del valor
        /// </summary>
        public string Lang { get; set; }
        /// <summary>
        /// Tipo de clave
        /// </summary>
        public string Tipo { get; set; }
    }
}