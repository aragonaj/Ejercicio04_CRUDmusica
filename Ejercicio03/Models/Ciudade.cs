using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Ciudade
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? PaisesId { get; set; }

    public virtual ICollection<Artista> Artista { get; set; } = new List<Artista>();

    public virtual ICollection<Concierto> Conciertos { get; set; } = new List<Concierto>();

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual Paise? Paises { get; set; }

    public virtual ICollection<Representante> Representantes { get; set; } = new List<Representante>();
}
