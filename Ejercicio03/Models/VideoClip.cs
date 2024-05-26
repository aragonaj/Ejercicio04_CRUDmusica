using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class VideoClip
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Título de la canción")]
    public int? CancionesId { get; set; }

    public DateOnly? Fecha { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public virtual Cancione? Canciones { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public virtual ICollection<VideoClipsPlataforma> VideoClipsPlataformas { get; set; } = new List<VideoClipsPlataforma>();
}
