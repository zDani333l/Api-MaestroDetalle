using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Api_MaestroDetalle.Models.Entities;

#nullable disable

namespace Api_MaestroDetalle.Models
{
    public partial class DBPruebaTecnicaContext : DbContext
    {
        public DBPruebaTecnicaContext()
        {
            this.Database.EnsureCreated();
        }

        public DBPruebaTecnicaContext(DbContextOptions<DBPruebaTecnicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.ToTable("Ciudad");

                entity.Property(e => e.Descripcion).HasMaxLength(60);
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.ToTable("Vendedor");

                entity.Property(e => e.Apellido).HasMaxLength(30);

                entity.Property(e => e.Nombre).HasMaxLength(30);

                entity.Property(e => e.NumeroIdentificacion).HasMaxLength(128);

                entity.HasOne(d => d.Ciudad)
                    .WithMany(p => p.Vendedor)
                    .HasForeignKey(d => d.IdCiudad)
                    .HasConstraintName("FK__Vendedor__IdCiud__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Api_MaestroDetalle.Models.Entities.CiudadDTO> Ciudad_1 { get; set; }
    }
}
