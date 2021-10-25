using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("Estados", Schema = "Catalogos")]
    public partial class Estado
    {
        public Estado()
        {
            Municipios = new HashSet<Municipio>();
        }

        [Key]
        public Guid Id { get; set; }
        [Column("Estado")]
        [StringLength(50)]
        public string Estado1 { get; set; }
        [StringLength(10)]
        public string Clave { get; set; }
        [StringLength(10)]
        public string Numero { get; set; }

        [InverseProperty(nameof(Municipio.IdEstadoNavigation))]
        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}
