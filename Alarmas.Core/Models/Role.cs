using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("Roles", Schema = "Seguridad")]
    public partial class Role
    {
        public Role()
        {
            Permisos = new HashSet<Permiso>();
            RolesFuncionalidades = new HashSet<RolesFuncionalidade>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string RolName { get; set; }
        [StringLength(150)]
        public string Descripcion { get; set; }

        [InverseProperty(nameof(Permiso.IdRolNavigation))]
        public virtual ICollection<Permiso> Permisos { get; set; }
        [InverseProperty(nameof(RolesFuncionalidade.IdRolNavigation))]
        public virtual ICollection<RolesFuncionalidade> RolesFuncionalidades { get; set; }
    }
}
