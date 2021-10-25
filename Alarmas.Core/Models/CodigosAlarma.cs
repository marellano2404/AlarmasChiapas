using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("CodigosAlarmas", Schema = "Catalogos")]
    public partial class CodigosAlarma
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
