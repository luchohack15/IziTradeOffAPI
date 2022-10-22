using IziTradeOff.Application.Interfaces;
using IziTradeOff.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IziTradeOff.Application.Security
{
    public class TokenService : ITokenService
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
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }
        /// <summary>
        /// Metodo encargado de crear nuevos tokens
        /// </summary>
        /// <param name="usuario">Entidad Usuario</param>
        /// <param name="roles">Roles del usuario</param>
        /// <returns>Nuevo Token</returns>
        /// Johnny Arcia
        public string CreateToken(Usuario usuario, IList<string> roles)
        {
            //Llaves que tendra el token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                //new Claim(JwtRegisteredClaimNames.Name, usuario.Nombre),
                //new Claim(JwtRegisteredClaimNames.FamilyName, usuario.Apellido),
                new Claim("username", usuario.UserName),
            };
            //Se agregan los roles al token
            if (roles != null && roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            //Se crea el acceso al sistema con una nueva llave
            var credencials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            //Descripcion y configuracion del token
            var tokenConfiguration = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(60),
                SigningCredentials = credencials,
                Issuer = _config["Token:Issuer"]
            };
            //Finalmente se procesa y crea el token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfiguration);

            return tokenHandler.WriteToken(token);
        }
    }
}
