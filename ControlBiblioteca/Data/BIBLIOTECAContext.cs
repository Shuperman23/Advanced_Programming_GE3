using ControlBiblioteca.DTOs;
using ControlBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlBiblioteca.Data;

public partial class BIBLIOTECAContext : DbContext
{
    public BIBLIOTECAContext()
    {
    }

    public BIBLIOTECAContext(DbContextOptions<BIBLIOTECAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<GeneroLiterario> GeneroLiterarios { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Parametro> Parametros { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            optionsBuilder.UseSqlServer(root.GetConnectionString("DevConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PK__AUTOR__F58AE909ACD96785");

            entity.ToTable("AUTOR");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
        });

        modelBuilder.Entity<GeneroLiterario>(entity =>
        {
            entity.HasKey(e => e.GeneroLiterarioID).HasName("PK__GENERO_L__5B6322709BE81B8B");

            entity.ToTable("GENERO_LITERARIO");

            entity.Property(e => e.GeneroLiterarioID).HasColumnName("GeneroLiterarioID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.LibroId).HasName("PK__LIBRO__35A1EC8D202E18EE");

            entity.ToTable("LIBRO");

            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.Estado)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.GeneroLiterarioId).HasColumnName("GeneroLiterarioID");
            entity.Property(e => e.NombreLibro)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.Autor).WithMany(p => p.Libros)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIBRO__AutorID__3C69FB99");

            entity.HasOne(d => d.GeneroLiterario).WithMany(p => p.Libros)
                .HasForeignKey(d => d.GeneroLiterarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIBRO__GeneroLit__3B75D760");
        });


        modelBuilder.Entity<Parametro>(entity =>
        {
            entity.HasKey(e => e.ParametroId).HasName("PK__PARAMETR__2B3CE672E26A000B");

            entity.ToTable("PARAMETRO");

            entity.Property(e => e.ParametroId).HasColumnName("ParametroID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Valor).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<StoredProcedureDto>(entity =>
        {
            entity.HasNoKey();
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
