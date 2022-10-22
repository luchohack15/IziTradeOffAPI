using System;
namespace IziTradeOff.Application.Interfaces
{
    public interface IUsuarioHelper
    {
        /// <summary>
        /// Obtiene el nombre del usuario logeado
        /// </summary>
        /// <returns>Nombre del usuario</returns>
        /// Johnny Arcia
        String ObtenerUsuarioSesion();
    }
}
