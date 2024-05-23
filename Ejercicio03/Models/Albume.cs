using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Albume
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? GenerosId { get; set; }

    public int? GruposId { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual ICollection<Cancione> Canciones { get; set; } = new List<Cancione>();

    public virtual Genero? Generos { get; set; }

    public virtual Grupo? Grupos { get; set; }
}
