using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dominio.Entidades;

public partial class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext()
    {
    }

    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditoria> Auditorias { get; set; }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Editoriale> Editoriales { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<EstadosExistencia> EstadosExistencias { get; set; }

    public virtual DbSet<Existencia> Existencias { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<LibrosAutore> LibrosAutores { get; set; }

    public virtual DbSet<LibrosTema> LibrosTemas { get; set; }

    public virtual DbSet<Paise> Paises { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Sancione> Sanciones { get; set; }

    public virtual DbSet<Tema> Temas { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<TiposPrestamo> TiposPrestamos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auditori__3214EC07B18DC4D6");

            entity.Property(e => e.DatosAntiguos).HasColumnName("Datos_Antiguos");
            entity.Property(e => e.DatosNuevos).HasColumnName("Datos_Nuevos");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Operacion).HasMaxLength(10);
            entity.Property(e => e.Tabla).HasMaxLength(50);
            entity.Property(e => e.UsuarioBd)
                .HasMaxLength(100)
                .HasDefaultValueSql("(suser_sname())")
                .HasColumnName("UsuarioBD");
        });

        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Autores__3214EC07BEB49E27");

            entity.Property(e => e.Nacionalidad).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Editoriale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Editoria__3214EC076466F2F6");

            entity.Property(e => e.NombreEditorial)
                .HasMaxLength(100)
                .HasColumnName("Nombre_Editorial");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estados__3214EC071AC9BD9F");

            entity.Property(e => e.NombreEstado)
                .HasMaxLength(100)
                .HasColumnName("Nombre_Estado");
        });

        modelBuilder.Entity<EstadosExistencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estados___3214EC07BB0AB8C5");

            entity.ToTable("Estados_Existencias");

            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Cambio");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.EstadosExistencia)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK__Estados_E__Estad__5535A963");

            entity.HasOne(d => d.ExistenciaNavigation).WithMany(p => p.EstadosExistencia)
                .HasForeignKey(d => d.Existencia)
                .HasConstraintName("FK__Estados_E__Exist__5441852A");
        });

        modelBuilder.Entity<Existencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Existenc__3214EC07606F008C");

            entity.HasIndex(e => e.CodigoEjemplar, "UQ__Existenc__EF52CF476FAFE413").IsUnique();

            entity.Property(e => e.CodigoEjemplar)
                .HasMaxLength(50)
                .HasColumnName("Codigo_Ejemplar");

            entity.HasOne(d => d.LibroNavigation).WithMany(p => p.Existencia)
                .HasForeignKey(d => d.Libro)
                .HasConstraintName("FK__Existenci__Libro__4F7CD00D");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libros__3214EC07B4B06E83");

            entity.ToTable(tb => tb.HasTrigger("trg_libros_insert"));

            entity.HasIndex(e => e.Isbn, "UQ__Libros__9271CED0D7A6E36C").IsUnique();

            entity.Property(e => e.Edicion).HasMaxLength(50);
            entity.Property(e => e.FechaLanzamiento).HasColumnName("Fecha_Lanzamiento");
            entity.Property(e => e.Isbn).HasMaxLength(20);
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.EditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Editorial)
                .HasConstraintName("FK__Libros__Editoria__4222D4EF");

            entity.HasOne(d => d.PaisNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Pais)
                .HasConstraintName("FK__Libros__Pais__4316F928");

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Tipo)
                .HasConstraintName("FK__Libros__Tipo__440B1D61");
        });

        modelBuilder.Entity<LibrosAutore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libros_A__3214EC07645D7C9F");

            entity.ToTable("Libros_Autores");

            entity.HasOne(d => d.AutorNavigation).WithMany(p => p.LibrosAutores)
                .HasForeignKey(d => d.Autor)
                .HasConstraintName("FK__Libros_Au__Autor__47DBAE45");

            entity.HasOne(d => d.LibroNavigation).WithMany(p => p.LibrosAutores)
                .HasForeignKey(d => d.Libro)
                .HasConstraintName("FK__Libros_Au__Libro__46E78A0C");
        });

        modelBuilder.Entity<LibrosTema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libros_T__3214EC07CF2E4237");

            entity.ToTable("Libros_Temas");

            entity.HasOne(d => d.LibroNavigation).WithMany(p => p.LibrosTemas)
                .HasForeignKey(d => d.Libro)
                .HasConstraintName("FK__Libros_Te__Libro__4AB81AF0");

            entity.HasOne(d => d.TemaNavigation).WithMany(p => p.LibrosTemas)
                .HasForeignKey(d => d.Tema)
                .HasConstraintName("FK__Libros_Tem__Tema__4BAC3F29");
        });

        modelBuilder.Entity<Paise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paises__3214EC07CBF480CE");

            entity.Property(e => e.NombrePais)
                .HasMaxLength(100)
                .HasColumnName("Nombre_Pais");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prestamo__3214EC0707A3806A");

            entity.ToTable(tb => tb.HasTrigger("trg_prestamos_insert"));

            entity.Property(e => e.FechaDevolucion).HasColumnName("Fecha_Devolucion");
            entity.Property(e => e.FechaEntregaReal).HasColumnName("Fecha_Entrega_Real");
            entity.Property(e => e.FechaPrestamo).HasColumnName("Fecha_Prestamo");
            entity.Property(e => e.TipoPrestamo).HasColumnName("Tipo_Prestamo");

            entity.HasOne(d => d.ExistenciaNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Existencia)
                .HasConstraintName("FK__Prestamos__Exist__60A75C0F");

            entity.HasOne(d => d.TipoPrestamoNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.TipoPrestamo)
                .HasConstraintName("FK__Prestamos__Tipo___5FB337D6");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__Prestamos__Usuar__5EBF139D");
        });

        modelBuilder.Entity<Sancione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sancione__3214EC075988BB93");

            entity.Property(e => e.FechaFin).HasColumnName("Fecha_Fin");
            entity.Property(e => e.FechaInicio).HasColumnName("Fecha_Inicio");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Sanciones)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__Sanciones__Usuar__6383C8BA");
        });

        modelBuilder.Entity<Tema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Temas__3214EC073F17B350");

            entity.Property(e => e.NombreTema)
                .HasMaxLength(100)
                .HasColumnName("Nombre_Tema");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipos__3214EC07477786D5");

            entity.Property(e => e.NombreTipo)
                .HasMaxLength(100)
                .HasColumnName("Nombre_Tipo");
        });

        modelBuilder.Entity<TiposPrestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipos_Pr__3214EC077F19F372");

            entity.ToTable("Tipos_Prestamos");

            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC075F06F0F6");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_usuarios_delete");
                    tb.HasTrigger("trg_usuarios_insert");
                    tb.HasTrigger("trg_usuarios_update");
                });

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A196EC8F4BE").IsUnique();

            entity.HasIndex(e => e.Documento, "UQ__Usuarios__AF73706DE2B0B737").IsUnique();

            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Documento).HasMaxLength(50);
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
