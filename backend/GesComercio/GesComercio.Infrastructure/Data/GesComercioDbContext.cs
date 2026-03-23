using GesComercio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GesComercio.Infrastructure.Data;

public class GesComercioDbContext : DbContext
{
    public GesComercioDbContext(DbContextOptions<GesComercioDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Comerciante> Comerciantes { get; set; }
    public DbSet<Establecimiento> Establecimientos { get; set; }
    public DbSet<Municipio> Municipios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //le dice a EF Core cómo mapear las entidades a las tablas de SQL Server
        base.OnModelCreating(modelBuilder);

        // Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.CorreoElectronico).IsUnique();
            entity.Property(e => e.Contrasena).HasMaxLength(255).IsRequired();
            entity.Property(e => e.Rol).HasMaxLength(30).IsRequired();
        });

        // Municipio
        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.ToTable("Municipio");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
        });

        // Comerciante
        modelBuilder.Entity<Comerciante>(entity =>
        {
            entity.ToTable("Comerciante");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RazonSocial).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.ActualizadoPor).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro).IsRequired();
            entity.Property(e => e.ActualizadoEn).IsRequired();

            // Relación Comerciante → Municipio
            entity.HasOne(e => e.Municipio)
                  .WithMany(m => m.Comerciantes)
                  .HasForeignKey(e => e.MunicipioId);
        });

        // Establecimiento
        modelBuilder.Entity<Establecimiento>(entity =>
        {
            entity.ToTable("Establecimiento");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Ingresos).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ActualizadoPor).HasMaxLength(100);
            entity.Property(e => e.ActualizadoEn).IsRequired();

            // Relación Establecimiento → Comerciante
            entity.HasOne(e => e.Comerciante)
                  .WithMany(c => c.Establecimientos)
                  .HasForeignKey(e => e.ComercianteId);
        });
    }
}