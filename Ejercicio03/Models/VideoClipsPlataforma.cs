using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class VideoClipsPlataforma
{
    public int Id { get; set; }

    public int? PlataformasId { get; set; }

    public int? VideoClipsId { get; set; }

    public string? Url { get; set; }

    public virtual Plataforma? Plataformas { get; set; }

    public virtual VideoClip? VideoClips { get; set; }
}
