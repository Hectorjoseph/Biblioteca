using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Estado
{
    public int Id { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<EstadosExistencia> EstadosExistencia { get; set; } = new List<EstadosExistencia>();
}
