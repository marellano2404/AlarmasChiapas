using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmasWPF.Core.ViewModels
{  
    public class Cliente
    {
        public Guid Id { get; set; }
        public int NumCliente { get; set; }
        public string Empresa { get; set; }
        public string Propietario { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string Referencias { get; set; }
        public string TelParticular { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public DateTime? FechaAlta { get; set; }

    }

    public class UsuarioVM
    {
        public Guid Id { get; set; }    
        public Guid IdCliente { get; set; }
        public int? NumUsuario { get; set; }
        public string NombreCompleto { get; set; }    
        public string Usuario { get; set; }    
        public string Contraseña { get; set; }    
        public string Puesto { get; set; }
        public DateTime? FechaAlta { get; set; }
    }
    public class InstalacionVM
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public int Zona { get; set; }
        public string LugarInstalacion { get; set; }
        public string Dispositivo { get; set; }

    }
}
