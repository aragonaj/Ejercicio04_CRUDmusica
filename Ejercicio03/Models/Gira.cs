using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Gira
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? GruposId { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public virtual ICollection<Concierto> Conciertos { get; set; } = new List<Concierto>();

    public virtual Grupo? Grupos { get; set; }
}
