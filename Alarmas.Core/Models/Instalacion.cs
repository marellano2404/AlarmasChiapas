using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("Instalacion", Schema = "Procesos")]
    public partial class Instalacion
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? IdCliente { get; set; }
        public int? Zona { get; set; }
        
        [StringLength(350)]
        public string LugarInstalacion { get; set; }
        
    }
}
