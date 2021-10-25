using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("Municipios", Schema = "Catalogos")]
    public partial class Municipio
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Municipio")]
        [StringLength(50)]
        public string Municipio1 { get; set; }
        public Guid? IdEstado { get; set; }

        [ForeignKey(nameof(IdEstado))]
        [InverseProperty(nameof(Estado.Municipios))]
        public virtual Estado IdEstadoNavigation { get; set; }
    }
}
