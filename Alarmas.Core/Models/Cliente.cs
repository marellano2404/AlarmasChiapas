using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Keyless]
    [Table("Clientes", Schema = "Procesos")]
    public partial class Cliente
    {
        public Guid Id { get; set; }
        public int NumCliente { get; set; }
        [StringLength(50)]
        public string Empresa { get; set; }
        [StringLength(50)]
        public string Propietario { get; set; }
        [Column("RFC")]
        [StringLength(13)]
        public string Rfc { get; set; }
        [StringLength(150)]
        public string Direccion { get; set; }
        [StringLength(150)]
        public string Referencias { get; set; }
        [StringLength(13)]
        public string TelParticular { get; set; }
        [StringLength(50)]
        public string Celular { get; set; }
        [StringLength(50)]
        public string Correo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FechaAlta { get; set; }
    }
}
