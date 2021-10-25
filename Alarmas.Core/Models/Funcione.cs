using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("Funciones", Schema = "Seguridad")]
    public partial class Funcione
    {
        public Funcione()
        {
            RolesFuncionalidades = new HashSet<RolesFuncionalidade>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        public Guid? IdPadre { get; set; }
        public bool? Status { get; set; }

        [InverseProperty(nameof(RolesFuncionalidade.IdFuncionalidadNavigation))]
        public virtual ICollection<RolesFuncionalidade> RolesFuncionalidades { get; set; }
    }
}
