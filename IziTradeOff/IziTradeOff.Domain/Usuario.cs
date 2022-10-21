using Microsoft.AspNetCore.Identity;

namespace IziTradeOff.Domain
{
    public class Usuario : IdentityUser
    {
        public string NombreCompleto { get; set; }
        public bool Activo { get; set; }
    }
}
