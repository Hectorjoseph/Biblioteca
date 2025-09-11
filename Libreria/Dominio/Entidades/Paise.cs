using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Paise
{
    public int Id { get; set; }

    public string NombrePais { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
