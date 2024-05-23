using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Genero
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Albume> Albumes { get; set; } = new List<Albume>();

    public virtual ICollection<Artista> Artista { get; set; } = new List<Artista>();

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
