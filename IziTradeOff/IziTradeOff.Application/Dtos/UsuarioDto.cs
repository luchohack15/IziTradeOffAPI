using System;
using System.Collections.Generic;
using System.Text;

namespace IziTradeOff.Application.Dtos
{
    public class UsuarioDto
    {
        public string NombreCompleto { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Imagen { get; set; }
    }
}
