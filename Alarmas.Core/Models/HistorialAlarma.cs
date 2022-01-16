using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("HistorialAlarmas", Schema = "Procesos")]
    public partial class HistorialAlarma
    {
        [Key]
        public Guid? Id { get; set; }
        public int IdClaveAlarma { get; set; }
        public Guid? IdCliente { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Descripcion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Fecha { get; set; }
        public string Hora { get; set; }
    }
}
