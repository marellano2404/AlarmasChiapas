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
        public Guid Id { get; set; }
        public int IdCodigo { get; set; }
        public Guid? IdCliente { get; set; }
        public int? IdTipo { get; set; }
        [StringLength(150)]
        public string Descripcion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaHora { get; set; }
    }
}
