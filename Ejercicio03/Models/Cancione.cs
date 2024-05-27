using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Cancione
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [Display(Name = "Título")]
    public string? Titulo { get; set; }

    [Display(Name = "Duración")]
    public TimeOnly? Duracion { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Álbum")]
    public int? AlbumesId { get; set; }

    public bool Single { get; set; }

    public virtual Albume? Albumes { get; set; }

    public virtual ICollection<VideoClip> VideoClips { get; set; } = new List<VideoClip>();
}
