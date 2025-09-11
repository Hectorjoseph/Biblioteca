using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Editoriale
{
    public int Id { get; set; }

    public string NombreEditorial { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
