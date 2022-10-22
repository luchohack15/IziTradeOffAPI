using IziTradeOff.Application.Dtos;
using IziTradeOff.Application.Interfaces.Services.Command;
using IziTradeOff.Application.Interfaces.Services.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace IziTradeOff.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class TraduccionController : ApiControllerBase
    {
        private ITraductorCommand _traductorCommand;
        private ITraductorQuery _traductorQuery;

        public TraduccionController(ITraductorCommand traductorCommand,ITraductorQuery traductorQuery)
        {
            _traductorCommand = traductorCommand;
            _traductorQuery = traductorQuery;
        }

        [HttpPost("CrearTraduccion")]
        public async Task<ActionResult<TraduccionDto>> CreateTipoPersona(TraduccionDto parametros)
        {
            return await _traductorCommand.CrearTraduccion(parametros);
        }

        [HttpGet("ObtenerTraduccion/{llave}/{lang}")]
        public async Task<ActionResult<TraduccionDto>> GetTraduccionXClave(string llave, string lang)
        {
            return await _traductorQuery.ObtenerTraduccion(llave,lang);
        }
    }
}
