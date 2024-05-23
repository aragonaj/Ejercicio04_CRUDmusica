using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Cancione
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public TimeOnly? Duracion { get; set; }

    public int? AlbumesId { get; set; }

    public bool Single { get; set; }

    public virtual Albume? Albumes { get; set; }

    public virtual ICollection<VideoClip> VideoClips { get; set; } = new List<VideoClip>();
}
