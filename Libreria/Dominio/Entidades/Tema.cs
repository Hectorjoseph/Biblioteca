using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Tema
{
    public int Id { get; set; }

    public string NombreTema { get; set; } = null!;

    public virtual ICollection<LibrosTema> LibrosTemas { get; set; } = new List<LibrosTema>();
}
