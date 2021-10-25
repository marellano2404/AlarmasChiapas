using System;

namespace Alarmas.Portal.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class JWTViewModel
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public Guid IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Rol { get; set; }
        public string UserName { get; set; }
        public string RFC { get; set; }
        public string CodigoPuesto { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }

    }
}
