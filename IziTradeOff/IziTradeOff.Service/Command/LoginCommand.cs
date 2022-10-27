using IziTradeOff.Application.Dtos;
using IziTradeOff.Application.Exceptions;
using IziTradeOff.Application.Interfaces;
using IziTradeOff.Domain;
using IziTradeOff.Persistence.Connection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using IziTradeOff.Application.Interfaces.Services.Command;

namespace IziTradeOff.Service.Command
{
    public class LoginCommand : ILoginCommand
    {
        #region "Variables Globales"
        private readonly IConexion _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IJwtGenerador _jwtGenerador;
        #endregion
        public LoginCommand(IConexion context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _jwtGenerador = jwtGenerador;
            _signInManager = signInManager;
        }
        /// <summary>
        /// Metodo que  sirve para logiarse
        /// </summary>
        public async Task<UsuarioDto> Login(string Email, string Password)
        {
            var _Usuario = await _userManager.FindByEmailAsync(Email);
            if (_Usuario == null)
            {
                throw new ExceptionBase(HttpStatusCode.Unauthorized);
            }
            var resultado = await _signInManager.CheckPasswordSignInAsync(_Usuario, Password, false);
            var resultadoRoles = await _userManager.GetRolesAsync(_Usuario);
            var listaRoles = new List<string>(resultadoRoles);
            if (resultado.Succeeded)
            {
                return new UsuarioDto
                {
                    NombreCompleto = _Usuario.NombreCompleto,
                    Token = _jwtGenerador.CrearToken(_Usuario, listaRoles),
                    UserName = _Usuario.UserName,
                    Email = _Usuario.Email,
                    Imagen = null
                };

            }
            throw new ExceptionBase(HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// Metodo que sirve para registrar un usuario 
        /// </summary>
        public async Task<UsuarioDto> Registar(UsuarioDto usuarioDto)
        {
            var existe = await _context.Users.Where(x => x.Email == usuarioDto.Email).AnyAsync();
            if (existe)
            {
                throw new ExceptionBase(HttpStatusCode.BadRequest, new { Mensaje = "Ya existe un usuario registrado con ese Email" });
            }
            var existeUserName = await _context.Users.Where(x => x.UserName == usuarioDto.UserName).AnyAsync();
            if (existeUserName)
            {
                throw new ExceptionBase(HttpStatusCode.BadRequest, new { Mensaje = "Ya existe un usuario registrado con ese Nombre de Usuario" });
            }
            var usuario = new Usuario
            {
                NombreCompleto = usuarioDto.NombreCompleto,
                Email = usuarioDto.Email,
                UserName = usuarioDto.UserName
            };

            var resultado = await _userManager.CreateAsync(usuario, usuarioDto.Password);

            if (resultado.Succeeded)
            {
                return new UsuarioDto
                {
                    NombreCompleto = usuario.NombreCompleto,
                    Token = _jwtGenerador.CrearToken(usuario, null),
                    UserName = usuario.UserName,
                    Email = usuario.Email
                };
            }

            throw new Exception("No se logro ingresar el Usuario");
        }
    }
}
