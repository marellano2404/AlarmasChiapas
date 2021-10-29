using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Alarmas.Core.Models
{
    public partial class CAlarmasDBContext : DbContext
    {
        public CAlarmasDBContext()
        {
        }

        public CAlarmasDBContext(DbContextOptions<CAlarmasDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ClienteUsuario> ClienteUsuarios { get; set; }
        public virtual DbSet<CodigosAlarma> CodigosAlarmas { get; set; }
        public virtual DbSet<TaskModel> TaskModels { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Funcione> Funciones { get; set; }
        public virtual DbSet<HistorialAlarma> HistorialAlarmas { get; set; }
        public virtual DbSet<HistorialUsuario> HistorialUsuarios { get; set; }
        public virtual DbSet<Instalacion> Instalacions { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<Permiso> Permisos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolesFuncionalidade> RolesFuncionalidades { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Helpers.ContextConfiguration.ConexionString, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.Empresa).IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NumCliente).ValueGeneratedOnAdd();

                entity.Property(e => e.Propietario).IsUnicode(false);

                entity.Property(e => e.Referencias).IsUnicode(false);

                entity.Property(e => e.Rfc).IsFixedLength(true);

                entity.Property(e => e.TelParticular).IsFixedLength(true);
            });

            modelBuilder.Entity<ClienteUsuario>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Contraseña).IsUnicode(false);

                entity.Property(e => e.NombreCompleto).IsUnicode(false);

                entity.Property(e => e.Puesto).IsFixedLength(true);

                entity.Property(e => e.Usuario).IsFixedLength(true);
            });

            modelBuilder.Entity<CodigosAlarma>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Clave).IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);
            });
            modelBuilder.Entity<TaskModel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.TaskName).IsUnicode(false);
                entity.Property(e => e.Description).IsUnicode(false);
                entity.Property(e => e.DueDate).IsUnicode(false);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Propietario).IsUnicode(false);

                entity.Property(e => e.Rfc).IsUnicode(false);

                entity.Property(e => e.Telefonos).IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Clave).IsFixedLength(true);

                entity.Property(e => e.Numero).IsFixedLength(true);
            });

            modelBuilder.Entity<Funcione>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<HistorialAlarma>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.IdCodigo).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<HistorialUsuario>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Describe).IsUnicode(false);

                entity.Property(e => e.Entradas).HasDefaultValueSql("((0))");

                entity.Property(e => e.FechaHora).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Validado).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Instalacion>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LugarInstalacion).IsUnicode(false);
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Municipio1).IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_Municipios_Estados");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permisos_Roles");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permisos_Usuarios");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<RolesFuncionalidade>(entity =>
            {
                entity.HasOne(d => d.IdFuncionalidadNavigation)
                    .WithMany(p => p.RolesFuncionalidades)
                    .HasForeignKey(d => d.IdFuncionalidad)
                    .HasConstraintName("FK_RolesFuncionalidades_Funcionalidades");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolesFuncionalidades)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_RolesFuncionalidades_Roles");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
