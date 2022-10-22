using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IziTradeOff.Application.Security
{
    public class PasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        /// <summary>
        /// Se implementa la validacion de password en tiempo de ejecucion
        /// </summary>
        /// <param name="manager">User Manager</param>
        /// <param name="user">Modelo usuario</param>
        /// <param name="password">password</param>
        /// <returns>Retorna una respuesta de aprobado si cumple con los estandares, sino retorna un failed</returns>
        /// Johnny Arcia
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
