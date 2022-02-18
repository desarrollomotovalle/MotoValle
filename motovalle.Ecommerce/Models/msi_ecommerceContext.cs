using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace motovalle.Ecommerce.Models
{
    public partial class msi_ecommerceContext : DbContext
    {
        public msi_ecommerceContext()
        {
        }

        public msi_ecommerceContext(DbContextOptions<msi_ecommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LavadoIlimitadoGratis> LavadoIlimitadoGratis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=74.208.211.144;Database=msi_ecommerce;User ID=pos_user;Password=inventek");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LavadoIlimitadoGratis>(entity =>
            {
                entity.ToTable("lavado_ilimitado_gratis");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(45);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasColumnName("cedula")
                    .HasMaxLength(45);

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasColumnName("celular")
                    .HasMaxLength(45);

                entity.Property(e => e.CiudadResidencia)
                    .IsRequired()
                    .HasColumnName("ciudad_residencia")
                    .HasMaxLength(45);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("correo")
                    .HasMaxLength(45);

                entity.Property(e => e.Factura)
                    .IsRequired()
                    .HasColumnName("factura")
                    .HasMaxLength(45);

                entity.Property(e => e.FechaFactura)
                    .HasColumnName("fecha_factura")
                    .HasColumnType("date");

                entity.Property(e => e.MarcaVehiculo)
                    .IsRequired()
                    .HasColumnName("marca_vehiculo")
                    .HasMaxLength(45);

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasColumnName("modelo")
                    .HasMaxLength(45);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .HasMaxLength(45);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasColumnName("placa")
                    .HasMaxLength(45);

                entity.Property(e => e.TratamientoDatos).HasColumnName("tratamiento_datos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
