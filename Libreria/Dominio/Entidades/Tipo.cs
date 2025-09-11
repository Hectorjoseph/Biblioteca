using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Tipo
{
    public int Id { get; set; }

    public string NombreTipo { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
