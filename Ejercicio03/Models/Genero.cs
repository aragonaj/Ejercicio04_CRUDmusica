using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Genero
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    public string? Nombre { get; set; }

    public virtual ICollection<Albume> Albumes { get; set; } = new List<Albume>();

    public virtual ICollection<Artista> Artista { get; set; } = new List<Artista>();

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
