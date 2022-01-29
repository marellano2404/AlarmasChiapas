using System;

namespace ViewModelReport
{
    public class AlarmasEmitidasVM
    {
        public string Id { get; set; }
        public int NumCliente { get; set; }
        public string Empresa { get; set; }
        public string ClaveAlarma { get; set; }
        public string Alarma { get; set; }
        public string Usuario { get; set; }
        public string DetalleAlarma { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
    }
}
