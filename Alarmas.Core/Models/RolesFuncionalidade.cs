using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("RolesFuncionalidades", Schema = "Seguridad")]
    public partial class RolesFuncionalidade
    {
        [Key]
        public int Id { get; set; }
        public Guid? IdRol { get; set; }
        public Guid? IdFuncionalidad { get; set; }

        [ForeignKey(nameof(IdFuncionalidad))]
        [InverseProperty(nameof(Funcione.RolesFuncionalidades))]
        public virtual Funcione IdFuncionalidadNavigation { get; set; }
        [ForeignKey(nameof(IdRol))]
        [InverseProperty(nameof(Role.RolesFuncionalidades))]
        public virtual Role IdRolNavigation { get; set; }
    }
}
