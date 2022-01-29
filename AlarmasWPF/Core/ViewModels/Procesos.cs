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
        public string Dispositivo { get; set; }
        public Guid? Id { get; set; }
        public Guid IdCliente { get; set; }
        public int Zona { get; set; }
        public string LugarInstalacion { get; set; }      

    }
    public class HistorialAlarmaVM
    {
        public Guid? Id { get; set; }
        public int IdClaveAlarma { get; set; }
        public Guid? IdCliente { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public Guid IdZonaInstalacion { get; set; }
        public string Hora { get; set; }
    }
    public class AlarmasEmitidasVM
    {
        public Guid Id { get; set; }
        public int NumCliente { get; set; }
        public string Empresa { get; set; }
        public string ClaveAlarma { get; set; }
        public string Alarma { get; set; }
        public string Usuario { get; set; }
        public string DetalleAlarma { get; set; }
        public DateTime? Fecha { get; set; }
        public string Hora { get; set; }

    }

}
