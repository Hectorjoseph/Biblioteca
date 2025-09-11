using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class EstadosExistencia
{
    public int Id { get; set; }

    public int? Existencia { get; set; }

    public int? Estado { get; set; }

    public DateTime? FechaCambio { get; set; }

    public virtual Estado? EstadoNavigation { get; set; }

    public virtual Existencia? ExistenciaNavigation { get; set; }
}
