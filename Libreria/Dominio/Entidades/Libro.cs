using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Libro
{
    public int Id { get; set; }

    public int? Editorial { get; set; }

    public int? Pais { get; set; }

    public int? Tipo { get; set; }

    public string? Isbn { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Edicion { get; set; }

    public DateOnly? FechaLanzamiento { get; set; }

    public virtual Editoriale? EditorialNavigation { get; set; }

    public virtual ICollection<Existencia> Existencia { get; set; } = new List<Existencia>();

    public virtual ICollection<LibrosAutore> LibrosAutores { get; set; } = new List<LibrosAutore>();

    public virtual ICollection<LibrosTema> LibrosTemas { get; set; } = new List<LibrosTema>();

    public virtual Paise? PaisNavigation { get; set; }

    public virtual Tipo? TipoNavigation { get; set; }
}
