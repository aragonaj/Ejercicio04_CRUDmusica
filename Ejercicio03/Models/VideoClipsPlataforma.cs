using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class VideoClipsPlataforma
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [Display(Name ="Plataforma")]
    public int? PlataformasId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [Display(Name = "Videoclip")]
    public int? VideoClipsId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(150, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [Url]
    public string? Url { get; set; }

    public virtual Plataforma? Plataformas { get; set; }

    public virtual VideoClip? VideoClips { get; set; }
}
