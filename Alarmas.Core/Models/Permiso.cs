using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("Permisos", Schema = "Seguridad")]
    public partial class Permiso
    {
        [Key]
        public int Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdRol { get; set; }

        [ForeignKey(nameof(IdRol))]
        [InverseProperty(nameof(Role.Permisos))]
        public virtual Role IdRolNavigation { get; set; }
        [ForeignKey(nameof(IdUser))]
        [InverseProperty(nameof(Usuario.Permisos))]
        public virtual Usuario IdUserNavigation { get; set; }
    }
}
