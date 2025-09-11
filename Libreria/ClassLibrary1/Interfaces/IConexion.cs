using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repositorio.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Usuario>? Usuarios { get; set; }
        DbSet<Libro>? Libros { get; set; }
        DbSet<Prestamo>? Prestamos { get; set; }
        DbSet<Editoriale>? Editoriales { get; set; }
        DbSet<Autore>? Autores { get; set; }
        DbSet<Tema>? Temas { get; set; }
        DbSet<Existencia>? Existencias { get; set; }
        DbSet<Auditoria>? Auditorias { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
