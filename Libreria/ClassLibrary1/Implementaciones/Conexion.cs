using Dominio.Entidades;
using Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        public Conexion() { }

        public Conexion(string connectionString)
        {
            this.StringConexion = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(this.StringConexion))
            {
                optionsBuilder.UseSqlServer(this.StringConexion, p => { });
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Libro>? Libros { get; set; }
        public DbSet<Prestamo>? Prestamos { get; set; }
        public DbSet<Editoriale>? Editoriales { get; set; }
        public DbSet<Autore>? Autores { get; set; }
        public DbSet<Tema>? Temas { get; set; }
        public DbSet<Existencia>? Existencias { get; set; }
        public DbSet<Auditoria>? Auditorias { get; set; }
        
    }
}

/*
public class Conexion : BibliotecaDbContext, IConexion
{
    public string? StringConexion { get; set; }

    public Conexion(string connectionString)
        : base(new DbContextOptionsBuilder<BibliotecaDbContext>()
              .UseSqlServer(connectionString)
              .Options)
    {
        StringConexion = connectionString;
    }
}
*/
