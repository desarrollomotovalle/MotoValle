using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace motovalle.Ecommerce.Models
{
    public partial class MOTOVALLEContext : DbContext
    {
        public MOTOVALLEContext()
        {
        }

        public MOTOVALLEContext(DbContextOptions<MOTOVALLEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TMercadeoBeneficiario> TMercadeoBeneficiario { get; set; }
        public virtual DbSet<TMercadeoClienteConvenio> TMercadeoClienteConvenio { get; set; }
        public virtual DbSet<TMercadeoClientes> TMercadeoClientes { get; set; }
        public virtual DbSet<TMercadeoConvenio> TMercadeoConvenio { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=192.168.0.64;Database=MOTOVALLE;User ID=SA;Password=Qazxsw21");
                optionsBuilder.UseSqlServer("Server=200.26.159.42;Database=MOTOVALLE;User ID=SA;Password=Qazxsw21");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TMercadeoBeneficiario>(entity =>
            {
                entity.HasKey(e => e.IdBeneficiario)
                    .HasName("t_Mercadeo_Beneficiario_PK");

                entity.ToTable("t_Mercadeo_Beneficiario");

                entity.HasIndex(e => e.TMercadeoConvenioIdConvenio)
                    .HasName("t_Mercadeo_Beneficiario__IDX")
                    .IsUnique();

                entity.Property(e => e.IdBeneficiario)
                    .HasColumnName("Id_Beneficiario")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NroDocumento)
                    .IsRequired()
                    .HasColumnName("Nro_Documento")
                    .HasMaxLength(20);

                entity.Property(e => e.TMercadeoConvenioIdConvenio).HasColumnName("t_Mercadeo_Convenio_Id_Convenio");

                entity.HasOne(d => d.TMercadeoConvenioIdConvenioNavigation)
                    .WithOne(p => p.TMercadeoBeneficiario)
                    .HasForeignKey<TMercadeoBeneficiario>(d => d.TMercadeoConvenioIdConvenio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_Mercadeo_Beneficiario_t_Mercadeo_Convenio_FK");
            });

            modelBuilder.Entity<TMercadeoClienteConvenio>(entity =>
            {
                entity.HasKey(e => e.IdClienteConvenio)
                    .HasName("t_Mercadeo_Cliente_convenio_PK");

                entity.ToTable("t_Mercadeo_Cliente_convenio");

                entity.Property(e => e.IdClienteConvenio).HasColumnName("Id_cliente_convenio");

                entity.Property(e => e.TMercadeoClientesIdCliente).HasColumnName("t_Mercadeo_Clientes_Id_Cliente");

                entity.Property(e => e.TMercadeoConvenioIdConvenio).HasColumnName("t_Mercadeo_Convenio_Id_Convenio");

                entity.HasOne(d => d.TMercadeoClientesIdClienteNavigation)
                    .WithMany(p => p.TMercadeoClienteConvenio)
                    .HasForeignKey(d => d.TMercadeoClientesIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_Mercadeo_Cliente_convenio_t_Mercadeo_Clientes_FK");

                entity.HasOne(d => d.TMercadeoConvenioIdConvenioNavigation)
                    .WithMany(p => p.TMercadeoClienteConvenio)
                    .HasForeignKey(d => d.TMercadeoConvenioIdConvenio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_Mercadeo_Cliente_convenio_t_Mercadeo_Convenio_FK");
            });

            modelBuilder.Entity<TMercadeoClientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("t_Mercadeo_Clientes_PK");

                entity.ToTable("t_Mercadeo_Clientes");

                entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");

                entity.Property(e => e.CiudadRegistro)
                    .IsRequired()
                    .HasColumnName("Ciudad_registro")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("Fecha_registro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Linea)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasColumnName("Nombre_completo")
                    .HasMaxLength(200);

                entity.Property(e => e.NroContacto)
                    .IsRequired()
                    .HasColumnName("Nro_Contacto")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NroDocumento)
                    .IsRequired()
                    .HasColumnName("Nro_documento")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TMercadeoConvenio>(entity =>
            {
                entity.HasKey(e => e.IdConvenio)
                    .HasName("t_Mercadeo_Convenio_PK");

                entity.ToTable("t_Mercadeo_Convenio");

                entity.Property(e => e.IdConvenio)
                    .HasColumnName("Id_Convenio")
                    .ValueGeneratedNever();

                entity.Property(e => e.Beneficio)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CodConvenio)
                    .IsRequired()
                    .HasColumnName("Cod_convenio")
                    .HasMaxLength(100);

                entity.Property(e => e.DesConvenio)
                    .IsRequired()
                    .HasColumnName("Des_convenio")
                    .HasMaxLength(200);

                entity.Property(e => e.NomPropietario)
                    .IsRequired()
                    .HasColumnName("Nom_propietario")
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
