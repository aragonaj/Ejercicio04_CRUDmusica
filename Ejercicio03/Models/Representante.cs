using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Representante
{
    public int Id { get; set; }

    public string? NombreCompleto { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Identificacion { get; set; }

    public string? Mail { get; set; }

    public string? Telefono { get; set; }

    public int? CiudadesId { get; set; }

    public virtual Ciudade? Ciudades { get; set; }

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
