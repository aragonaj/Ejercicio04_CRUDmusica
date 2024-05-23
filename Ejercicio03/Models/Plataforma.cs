using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Plataforma
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<VideoClipsPlataforma> VideoClipsPlataformas { get; set; } = new List<VideoClipsPlataforma>();
}
