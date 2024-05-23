using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Paise
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Ciudade> Ciudades { get; set; } = new List<Ciudade>();
}
