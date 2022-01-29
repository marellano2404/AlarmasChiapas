using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmasWPF.Core.ViewModels
{
    public class ReportViewModel
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