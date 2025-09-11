using System;
using System.Collections.Generic;

namespace Dominio.Entidades;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Documento { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Correo { get; set; }

    public string Contraseña { get; set; } = null!;

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Sancione> Sanciones { get; set; } = new List<Sancione>();
}
