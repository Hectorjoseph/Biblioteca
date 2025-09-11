using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class LibrosTema
{
    public int Id { get; set; }

    public int? Libro { get; set; }

    public int? Tema { get; set; }

    public virtual Libro? LibroNavigation { get; set; }

    public virtual Tema? TemaNavigation { get; set; }
}
