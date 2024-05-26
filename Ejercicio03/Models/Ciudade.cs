using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Ciudade
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "País")]
    public int? PaisesId { get; set; }

    public virtual ICollection<Artista> Artista { get; set; } = new List<Artista>();

    public virtual ICollection<Concierto> Conciertos { get; set; } = new List<Concierto>();

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual Paise? Paises { get; set; }

    public virtual ICollection<Representante> Representantes { get; set; } = new List<Representante>();
}
