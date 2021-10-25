using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("Usuarios", Schema = "Seguridad")]
    public partial class Usuario
    {
        public Usuario()
        {
            Permisos = new HashSet<Permiso>();
        }

        [Key]
        public Guid Id { get; set; }
        public Guid? IdEmpleado { get; set; }
        [StringLength(250)]
        public string NombreCompleto { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(60)]
        public string Password { get; set; }
        public bool? Habilitado { get; set; }

        [InverseProperty(nameof(Permiso.IdUserNavigation))]
        public virtual ICollection<Permiso> Permisos { get; set; }
    }
}
