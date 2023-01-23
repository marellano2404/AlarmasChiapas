using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarmas.Core.ViewModels
{
    public class ResultVM
    {
        public bool Exito { get; set; }
        public string Result { get; set; }

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
    public class ClientesVM
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
        public int Users { get; set; }
        public int Inst { get; set; }
        public int Alarmas { get; set; }

    }


}
