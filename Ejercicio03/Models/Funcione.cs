using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Funcione
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<FuncionesArtista> FuncionesArtista { get; set; } = new List<FuncionesArtista>();
}
