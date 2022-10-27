using IziTradeOff.Application.Dtos;
using IziTradeOff.Application.Interfaces.Services.Command;
using IziTradeOff.Application.Interfaces.Services.Query;
using IziTradeOff.Domain;
using IziTradeOff.Service.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IziTradeOff.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ApiControllerBase
    {

        private ILoginCommand _loginCommand;
        private ILoginQuery _loginQuery;

        public UsuarioController(ILoginCommand loginCommand)
        {
            _loginCommand = loginCommand;

        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(string Email, string Password)
        {
            return await _loginCommand.Login(Email, Password);
        }
        [HttpPost("Registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(UsuarioDto usuarioDto)
        {
            return await _loginCommand.Registar(usuarioDto);
        }
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> DevolverUsuario()
        {
            return await _loginQuery.UsuarioActual();
        }
    }
}
