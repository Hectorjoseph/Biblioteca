using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Auditoria
{
    public int Id { get; set; }

    public string Tabla { get; set; } = null!;

    public string Operacion { get; set; } = null!;

    public string? UsuarioBd { get; set; }

    public DateTime? Fecha { get; set; }

    public string? DatosAntiguos { get; set; }

    public string? DatosNuevos { get; set; }
}
