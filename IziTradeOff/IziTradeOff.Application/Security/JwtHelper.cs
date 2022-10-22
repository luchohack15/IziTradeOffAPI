using IziTradeOff.Application.Interfaces;
using IziTradeOff.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IziTradeOff.Application.Security
{
    public class JwtHelper : IJwtGenerador
    {
        //Llave simetrica
        private readonly SymmetricSecurityKey _key;
        /// <summary>
        /// Configuracion del entorno
        /// </summary>
        private readonly IConfiguration _config;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="config">Configuracion del entorno</param>
        public JwtHelper(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }
        /// <summary>
        /// Metodo encargado de crear token del usuario
        /// </summary>
        /// <param name="usuario">Objeto Usuario</param>
        /// <param name="roles">Lista de nombres de roles para el usuario</param>
        /// <returns>Token de conexion</returns>
        /// <remarks>Johnny Arcia</remarks>
        public string CrearToken(Usuario usuario, List<string> roles)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("username", usuario.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };
            if (roles != null)
            {
                foreach (var rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }
            var credenciales = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciales,
                Issuer = _config["Token:Issuer"]
            };

            var tokenManejador = new JwtSecurityTokenHandler();
            var token = tokenManejador.CreateToken(tokenDescripcion);

            return tokenManejador.WriteToken(token);
        }
    }
}
