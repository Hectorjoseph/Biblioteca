using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Prestamo
{
    public int Id { get; set; }

    public int? Usuario { get; set; }

    public int? TipoPrestamo { get; set; }

    public int? Existencia { get; set; }

    public DateOnly FechaPrestamo { get; set; }

    public DateOnly? FechaDevolucion { get; set; }

    public DateOnly? FechaEntregaReal { get; set; }

    public virtual Existencia? ExistenciaNavigation { get; set; }

    public virtual TiposPrestamo? TipoPrestamoNavigation { get; set; }

    public virtual Usuario? UsuarioNavigation { get; set; }
}
