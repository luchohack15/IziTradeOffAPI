using IziTradeOff.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IziTradeOff.Application.Interfaces.Services.Command
{
    public interface ILoginCommand
    {
        Task<UsuarioDto> Registar(UsuarioDto usuarioDto);
        Task<UsuarioDto> Login(string Email, string Password);
    }
}
