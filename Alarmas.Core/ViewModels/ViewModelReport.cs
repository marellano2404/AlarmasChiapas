using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarmas.Core.ViewModels
{
    public class ViewModelReportAlarmas
    {
        public string ClaveAlarma { get; set; }
        public string Alarma { get; set; }
        public int NumUsuario { get; set; }    
        public string Usuario { get; set; }
        public string DetalleAlarma { get; set; }
        public int Zona { get; set; }
        public string LugarInstalacion { get; set; }
        public string Dispositivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
    }
    public class DatoReporte
    {
        public Guid IdCliente { get; set; }
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
    }
}
