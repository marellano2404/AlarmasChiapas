using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("Empresa", Schema = "Estructura")]
    public partial class Empresa
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Propietario { get; set; }
        [StringLength(50)]
        public string Direccion { get; set; }
        [Column("RFC")]
        [StringLength(50)]
        public string Rfc { get; set; }
        [StringLength(150)]
        public string Telefonos { get; set; }
    }
}
