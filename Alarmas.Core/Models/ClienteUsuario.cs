using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Alarmas.Core.Models
{
    [Table("ClienteUsuarios", Schema = "Procesos")]
    public partial class ClienteUsuario
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? IdCliente { get; set; }
        [StringLength(250)]
        public string NombreCompleto { get; set; }
        [StringLength(100)]
        public string Usuario { get; set; }
        [StringLength(50)]
        public string Contraseña { get; set; }
        [StringLength(25)]
        public string Puesto { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
