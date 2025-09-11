using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Existencia
{
    public int Id { get; set; }

    public int? Libro { get; set; }

    public string CodigoEjemplar { get; set; } = null!;

    public virtual ICollection<EstadosExistencia> EstadosExistencia { get; set; } = new List<EstadosExistencia>();

    public virtual Libro? LibroNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
