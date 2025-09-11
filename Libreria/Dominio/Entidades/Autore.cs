using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Autore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Nacionalidad { get; set; }

    public virtual ICollection<LibrosAutore> LibrosAutores { get; set; } = new List<LibrosAutore>();
}
