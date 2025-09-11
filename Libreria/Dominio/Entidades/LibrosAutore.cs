using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class LibrosAutore
{
    public int Id { get; set; }

    public int? Libro { get; set; }

    public int? Autor { get; set; }

    public virtual Autore? AutorNavigation { get; set; }

    public virtual Libro? LibroNavigation { get; set; }
}
