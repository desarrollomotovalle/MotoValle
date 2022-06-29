using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace motovalle.Ecommerce.Models
{
    public partial class motovalle_petronasContext : DbContext
    {
        public motovalle_petronasContext()
        {
        }

        public motovalle_petronasContext(DbContextOptions<motovalle_petronasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PortafolioLubricanteAutomotor> PortafolioLubricanteAutomotor { get; set; }
        public virtual DbSet<PortafolioLubricanteAutomotorDetalle> PortafolioLubricanteAutomotorDetalle { get; set; }
        public virtual DbSet<PortafolioLubricantes> PortafolioLubricantes { get; set; }
        public virtual DbSet<PortafolioLubricantesDetalle> PortafolioLubricantesDetalle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
           {
                optionsBuilder.UseMySql("Server=127.0.0.1;Database=motovalle_petronas;User ID=pos_user;Password=inventek");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortafolioLubricanteAutomotor>(entity =>
            {
                entity.HasKey(e => e.IdPortafolioAutomotor)
                    .HasName("PRIMARY");

                entity.ToTable("portafolio_lubricante_automotor");

                entity.HasIndex(e => e.IdDetallePortafolioAutomotor)
                    .HasName("fk_portafolioautomotriz_detalleportafolioautomotriz_id_idx");

                entity.Property(e => e.IdPortafolioAutomotor).HasColumnName("id_portafolio_automotor");

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasColumnName("categoria")
                    .HasMaxLength(45);

                entity.Property(e => e.DescripcionProducto1)
                    .HasColumnName("descripcion_producto1")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto2)
                    .HasColumnName("descripcion_producto2")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto3)
                    .HasColumnName("descripcion_producto3")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto4)
                    .HasColumnName("descripcion_producto4")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto5)
                    .HasColumnName("descripcion_producto5")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto6)
                    .HasColumnName("descripcion_producto6")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto7)
                    .HasColumnName("descripcion_producto7")
                    .HasMaxLength(250);

                entity.Property(e => e.GamaProducto)
                    .HasColumnName("gama_producto")
                    .HasMaxLength(250);

                entity.Property(e => e.IdDetallePortafolioAutomotor).HasColumnName("id_detalle_portafolio_automotor");

                entity.Property(e => e.ReferenciaProducto)
                    .HasColumnName("referencia_producto")
                    .HasMaxLength(50);

                entity.Property(e => e.TipoAceite)
                    .HasColumnName("tipo_aceite")
                    .HasMaxLength(250);

                entity.Property(e => e.UrlImagen)
                    .HasColumnName("url_imagen")
                    .HasMaxLength(250);

                entity.Property(e => e.ViscosidadAceite)
                    .HasColumnName("viscosidad_aceite")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdDetallePortafolioAutomotorNavigation)
                    .WithMany(p => p.PortafolioLubricanteAutomotor)
                    .HasForeignKey(d => d.IdDetallePortafolioAutomotor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_portafolioautomotor_detalleportafolioautomotor_id");
            });

            modelBuilder.Entity<PortafolioLubricanteAutomotorDetalle>(entity =>
            {
                entity.HasKey(e => e.IdDetallePortafolioAutomotor)
                    .HasName("PRIMARY");

                entity.ToTable("portafolio_lubricante_automotor_detalle");

                entity.Property(e => e.IdDetallePortafolioAutomotor).HasColumnName("id_detalle_portafolio_automotor");

                entity.Property(e => e.Caracteristica1)
                    .HasColumnName("caracteristica1")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica2)
                    .HasColumnName("caracteristica2")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica3)
                    .HasColumnName("caracteristica3")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica4)
                    .HasColumnName("caracteristica4")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica5)
                    .HasColumnName("caracteristica5")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica6)
                    .HasColumnName("caracteristica6")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica7)
                    .HasColumnName("caracteristica7")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionAplicaciones).HasColumnName("descripcion_aplicaciones");

                entity.Property(e => e.DescripcionSalud).HasColumnName("descripcion_salud");

                entity.Property(e => e.DescripcionTituloDetalle).HasColumnName("descripcion_titulo_detalle");

                entity.Property(e => e.Especificacion1)
                    .HasColumnName("especificacion1")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion2)
                    .HasColumnName("especificacion2")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion3)
                    .HasColumnName("especificacion3")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion4)
                    .HasColumnName("especificacion4")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion5)
                    .HasColumnName("especificacion5")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion6)
                    .HasColumnName("especificacion6")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion7)
                    .HasColumnName("especificacion7")
                    .HasMaxLength(250);

                entity.Property(e => e.TituloCaracteristicas)
                    .HasColumnName("titulo_caracteristicas")
                    .HasMaxLength(250);

                entity.Property(e => e.TituloDescripcionAplicaciones).HasColumnName("titulo_descripcion_aplicaciones");

                entity.Property(e => e.TituloDetalle)
                    .HasColumnName("titulo_detalle")
                    .HasMaxLength(250);

                entity.Property(e => e.TituloEspecificaciones)
                    .HasColumnName("titulo_especificaciones")
                    .HasMaxLength(250);

                entity.Property(e => e.TituloSeguridad)
                    .HasColumnName("titulo_seguridad")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<PortafolioLubricantes>(entity =>
            {
                entity.HasKey(e => e.IdPortafolioAutomotriz)
                    .HasName("PRIMARY");

                entity.ToTable("portafolio_lubricantes");

                entity.HasIndex(e => e.IdDetallePortafolioAutomotriz)
                    .HasName("fk_portafolioautomotriz_detalleportafolioautomotriz_id_idx");

                entity.Property(e => e.IdPortafolioAutomotriz).HasColumnName("id_portafolio_automotriz");

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasColumnName("categoria")
                    .HasMaxLength(45);

                entity.Property(e => e.DescripcionProducto1)
                    .HasColumnName("descripcion_producto1")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto2)
                    .HasColumnName("descripcion_producto2")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto3)
                    .HasColumnName("descripcion_producto3")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto4)
                    .HasColumnName("descripcion_producto4")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto5)
                    .HasColumnName("descripcion_producto5")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto6)
                    .HasColumnName("descripcion_producto6")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionProducto7)
                    .HasColumnName("descripcion_producto7")
                    .HasMaxLength(250);

                entity.Property(e => e.GamaProducto)
                    .HasColumnName("gama_producto")
                    .HasMaxLength(250);

                entity.Property(e => e.IdDetallePortafolioAutomotriz).HasColumnName("id_detalle_portafolio_automotriz");

                entity.Property(e => e.ReferenciaProducto)
                    .HasColumnName("referencia_producto")
                    .HasMaxLength(50);

                entity.Property(e => e.TipoAceite)
                    .HasColumnName("tipo_aceite")
                    .HasMaxLength(250);

                entity.Property(e => e.UrlImagen)
                    .HasColumnName("url_imagen")
                    .HasMaxLength(250);

                entity.Property(e => e.ViscosidadAceite)
                    .HasColumnName("viscosidad_aceite")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdDetallePortafolioAutomotrizNavigation)
                    .WithMany(p => p.PortafolioLubricantes)
                    .HasForeignKey(d => d.IdDetallePortafolioAutomotriz)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_portafolioautomotriz_detalleportafolioautomotriz_id");
            });

            modelBuilder.Entity<PortafolioLubricantesDetalle>(entity =>
            {
                entity.HasKey(e => e.IdDetallePortafolioAutomotriz)
                    .HasName("PRIMARY");

                entity.ToTable("portafolio_lubricantes_detalle");

                entity.Property(e => e.IdDetallePortafolioAutomotriz).HasColumnName("id_detalle_portafolio_automotriz");

                entity.Property(e => e.Caracteristica1)
                    .HasColumnName("caracteristica1")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica2)
                    .HasColumnName("caracteristica2")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica3)
                    .HasColumnName("caracteristica3")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica4)
                    .HasColumnName("caracteristica4")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica5)
                    .HasColumnName("caracteristica5")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica6)
                    .HasColumnName("caracteristica6")
                    .HasMaxLength(250);

                entity.Property(e => e.Caracteristica7)
                    .HasColumnName("caracteristica7")
                    .HasMaxLength(250);

                entity.Property(e => e.DescripcionAplicaciones).HasColumnName("descripcion_aplicaciones");

                entity.Property(e => e.DescripcionSalud).HasColumnName("descripcion_salud");

                entity.Property(e => e.DescripcionTituloDetalle).HasColumnName("descripcion_titulo_detalle");

                entity.Property(e => e.Especificacion1)
                    .HasColumnName("especificacion1")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion2)
                    .HasColumnName("especificacion2")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion3)
                    .HasColumnName("especificacion3")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion4)
                    .HasColumnName("especificacion4")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion5)
                    .HasColumnName("especificacion5")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion6)
                    .HasColumnName("especificacion6")
                    .HasMaxLength(250);

                entity.Property(e => e.Especificacion7)
                    .HasColumnName("especificacion7")
                    .HasMaxLength(250);

                entity.Property(e => e.TituloCaracteristicas)
                    .HasColumnName("titulo_caracteristicas")
                    .HasMaxLength(250);

                entity.Property(e => e.TituloDescripcionAplicaciones).HasColumnName("titulo_descripcion_aplicaciones");

                entity.Property(e => e.TituloDetalle)
                    .HasColumnName("titulo_detalle")
                    .HasMaxLength(250);

                entity.Property(e => e.TituloEspecificaciones)
                    .HasColumnName("titulo_especificaciones")
                    .HasMaxLength(250);

                entity.Property(e => e.TituloSeguridad)
                    .HasColumnName("titulo_seguridad")
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
