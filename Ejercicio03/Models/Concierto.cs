using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Concierto
{
    public int Id { get; set; }

    public int? GirasId { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? CiudadesId { get; set; }

    public string? Direccion { get; set; }

    public virtual Ciudade? Ciudades { get; set; }

    public virtual Gira? Giras { get; set; }
}
