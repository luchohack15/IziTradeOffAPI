using IziTradeOff.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IziTradeOff.Application.Interfaces.Services
{
    public interface ITraduccionService
    {
        /// <summary>
        /// Agrega una nueva traduccion
        /// </summary>
        /// <param name="traduccion">Traduccion Dto</param>
        /// <returns>Traduccion con Id</returns>
        /// Johnny Arcia
        Task<TraduccionDto> AddTraduccionAsync(TraduccionDto traduccion);
        /// <summary>
        /// Obtiene todas las traducciones en todos los lenguajes
        /// </summary>
        /// <returns>Obtiene todas las traducciones</returns>
        /// Johnny Arcia
        Task<IReadOnlyList<TraduccionDto>> GetAllTraduccionesAsync();
        /// <summary>
        /// Obtiene la traduccion de la clave
        /// </summary>
        /// <param name="clave">Clave de la traduccion</param>
        /// <returns>Retorna la traduccion que contienen la clave</returns>
        /// Johnny Arcia
        Task<TraduccionDto> GetTraduccionPorClaveAsync(string llave, string lang);
    }
}
