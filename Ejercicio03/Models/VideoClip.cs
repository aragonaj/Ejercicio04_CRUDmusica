using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class VideoClip
{
    public int Id { get; set; }

    public int? CancionesId { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual Cancione? Canciones { get; set; }

    public virtual ICollection<VideoClipsPlataforma> VideoClipsPlataformas { get; set; } = new List<VideoClipsPlataforma>();
}
